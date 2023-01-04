using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.Core.Services;
using XFin.API.DAL.DbContexts;
using XFin.API.DAL.Interfaces;

namespace XFin.API.DAL.Repositories
{
    public class RecurringTransactionRepository : IRecurringTransactionRepository
    {
        public RecurringTransactionRepository(XFinDbContext context, ITransactionService calculator, IMapper mapper)
        {
            this.calculator = calculator;
            this.context = context;
            this.mapper = mapper;
        }

        public RecurringTransactionModel Create(RecurringTransactionCreationModel transaction)
        {
            if (transaction.SourceCostCenterId == null)
            {
                transaction.SourceCostCenterId = context.CostCenters
                    .Where(c => c.Name == "Nicht zugewiesen")
                    .FirstOrDefault()
                    .Id;
            }

            if (transaction.TargetCostCenterId == null)
            {
                transaction.TargetCostCenterId = context.CostCenters
                    .Where(c => c.Name == "Nicht zugewiesen")
                    .FirstOrDefault()
                    .Id;
            }

            var newTransaction = mapper.Map<RecurringTransaction>(transaction);

            context.RecurringTransactions.Add(newTransaction);
            context.SaveChanges();

            //this prevents an object cycle 500 internal server error
            //newTransaction.CostCenter = null;
            //newTransaction.InternalBankAccount = null;

            return mapper.Map<RecurringTransactionModel>(newTransaction);
        }

        public List<RecurringTransactionModel> GetAllBySourceAccount(int accountId)
        {
            var recurringTransactions = mapper.Map<List<RecurringTransactionModel>>(context.RecurringTransactions
                .Where(r => r.SourceBankAccountId == accountId)
                .Include(r => r.SourceBankAccount)
                .Include(r => r.TargetBankAccount)
                .Include(r => r.SourceCostCenter)
                .Include(r => r.TargetCostCenter)
                .Include(r => r.Transactions)
                .ToList());

            foreach (var recurringTransaction in recurringTransactions)
            {
                recurringTransaction.TargetBankAccount.AccountHolderName = context.AccountHolders
                    .Where(a => a.Id == recurringTransaction.TargetBankAccount.AccountHolderId)
                    .FirstOrDefault()
                    .Name;

                recurringTransaction.SourceBankAccount.AccountHolderName = context.AccountHolders
                    .Where(a => a.Id == recurringTransaction.SourceBankAccount.AccountHolderId)
                    .FirstOrDefault()
                    .Name;
            }

            return recurringTransactions.Where(r => DateTime.Compare(r.EndDate, DateTime.Today) > 0).ToList();
        }

        public List<RecurringTransactionModel> GetAllByTargetAccount(int accountId)
        {
            var recurringTransactions = mapper.Map<List<RecurringTransactionModel>>(context.RecurringTransactions
                .Where(r => r.TargetBankAccountId == accountId)
                .Include(r => r.SourceBankAccount)
                .Include(r => r.SourceCostCenter)
                .Include(r => r.TargetBankAccount)
                .Include(r => r.TargetCostCenter)
                .Include(r => r.Transactions)
                .ToList());

            foreach (var recurringTransaction in recurringTransactions)
            {
                recurringTransaction.SourceBankAccount.AccountHolderName = context.AccountHolders
                    .Where(a => a.Id == recurringTransaction.SourceBankAccount.AccountHolderId)
                    .FirstOrDefault()
                    .Name;

                recurringTransaction.TargetBankAccount.AccountHolderName = context.AccountHolders
                    .Where(a => a.Id == recurringTransaction.TargetBankAccount.AccountHolderId)
                    .FirstOrDefault()
                    .Name;
            }

            return recurringTransactions;
        }

        public List<RecurringTransactionModel> GetAllByDueDate(int year, int month, int day)
        {
            var recurringTransactions = context.RecurringTransactions
                .Include(r => r.SourceBankAccount)
                .Include(r => r.SourceCostCenter)
                .Include(r => r.TargetBankAccount)
                .Include(r => r.TargetCostCenter)
                .Include(r => r.Transactions)
                .ToList();

            var recurringTransactionModels = new List<RecurringTransactionModel>();

            var dueDate = new DateTime(year, month, day);


            foreach (var transaction in recurringTransactions)
            {
                //if StartDate is earlier than dueDate && dueDate is earlier than EndDate -> check if the transaction is due in the given month
                if (TransactionIsDue(transaction, dueDate))
                {
                    var cycle = transaction.Cycle;
                    var transactionsPerYear = 12 / cycle;

                    for (int i = 0; i < transactionsPerYear; i++)
                    {
                        var transactionDueMonth = transaction.StartDate.Month + i * cycle;
                        transactionDueMonth = transactionDueMonth > 12 ? transactionDueMonth - 12 : transactionDueMonth;


                        if (transactionDueMonth == month && transaction.DayOfMonth <= day)
                        {
                            //TODO - die if-Abfrage kann Probleme machen, wenn eine Transaction im letzten Monat fällig war und bereits der neue Monat ist
                            //TODO - z.B. wenn am Ende des Monats eine regelmäßige Transaktion fällig ist, diese aber aufgrund von WE oder Feiertag erst im nächsten Monat verbucht wird
                            //TODO - da muss ich mir noch was überlegen
                            //if no transaction is found in the transactions of the recurringTransaction, add it to the list and break the loop
                            if (transaction.Transactions.Where(t => t.DueDate.Month == month && t.DueDate.Day <= day).FirstOrDefault() == null)
                            {
                                var recurringTransactionModel = mapper.Map<RecurringTransactionModel>(transaction);

                                recurringTransactionModel.SourceBankAccount.AccountHolderName = context.AccountHolders
                                    .Where(a => a.Id == recurringTransactionModel.SourceBankAccount.AccountHolderId).FirstOrDefault().Name;

                                recurringTransactionModel.SourceBankAccount.AccountNumber = calculator.GetAccountNumber(recurringTransactionModel.SourceBankAccount.Iban);

                                recurringTransactionModel.TargetBankAccount.AccountHolderName = context.AccountHolders
                                    .Where(a => a.Id == recurringTransactionModel.TargetBankAccount.AccountHolderId).FirstOrDefault().Name;

                                recurringTransactionModel.TargetBankAccount.AccountNumber = calculator.GetAccountNumber(recurringTransactionModel.TargetBankAccount.Iban);

                                recurringTransactionModel.DueDate = new DateTime(year, transactionDueMonth, transaction.DayOfMonth);

                                recurringTransactionModels.Add(recurringTransactionModel);
                                break;
                            }

                            //transaction is due in the given month and on the given day
                            //TODO - hier muss ich beachten, dass eine Transaktion vielleicht erst ein paar Tage später im Konto verbucht wird (also im Online-Banking, im richtigen Konto)
                            //TODO - wenn z.B. der 15. ein Sonntag ist, findet die Transaktion erst am Montag, 16. statt und soll im Haushaltsbuch auch erst dann verbucht werden
                            //TODO - das bedeutet, dass die Notification im Frontend nicht nur an genau dem Fälligkeitstag angezeigt werden soll, sondern unter Umständen auch noch am nächsten
                            //TODO - Tag oder sogar mehrere Tage später
                            //TODO - der User muss die Transaktion per Klick bestätigen, damit sie verbucht wird - kann sie aber auch einmal aussetzen, falls das Konto nicht gedeckt war
                            //TODO - außerdem kann er auch nochmal den Betrag oder Verwendungszweck ändern, wenn er möchte
                            //TODO - ich brauche also eine Möglichkeit, festzustellen, ob die Transaktion in diesem Monat bereits ausgeführt wurde, wenn ja, verschwindet die Notification im
                            //TODO - Frontend - wenn nein, wird sie auch am nächsten Tag noch angezeigt.
                            //TODO
                            //TODO - L Ö S U N G
                            //TODO
                            //TODO - jede Transaction bekommt eine Property DueDate -> die wird bei normalen Transactions auf das gleiche Datum wie die Property Date gesetzt
                            //TODO - bei Transactions, die mit einer RecurringTransaction verlinkt sind, kommt in DueDate aber das DueDate der RecurringTransaction rein, so kann man auch
                            //TODO - noch ein paar Tage später oder gar im nächsten Monat feststellen, dass diese Transaction, die dann in der Property Transactions vorkommt, die Transactions
                            //TODO - zu dem entsprechenden Fälligkeitsdatum ist -> das Fälligkeitsdatum wird von dieser Methode zum RecurringTransactionModel hinzugefügt (über month und day)
                            //TODO - und ist so im Frontend verfügbar, um es in die Transaction zu schreiben, wenn sie dann Tage später bestätigt wird
                            //TODO
                            //TODO - wird sie im Frontend einmalig gecancelt, wird die Property 'Executed' der Transaction auf false gesetzt und die Transaction landet auch in der Liste
                            //TODO - so wird hier eine Transaction mit dem Fälligkeitsdatum gefunden, die aber gecancelt und nicht ausgeführt wurde
                            //TODO
                            //TODO - W I C H T I G
                            //TODO
                            //TODO - durch die neue Property 'Executed' bei Transaction muss ich beim Abrufen von Transactions und Kalkulieren von Balance / Einnahmen / Ausgaben / etc.
                            //TODO - darauf achten, die Transactions, die nicht ausgeführt wurden, wo also 'Executed' auf false steht, NICHT in der Berechnung zu berücksichtigen!!!
                            //recurringTransactionModels.Add(mapper.Map<RecurringTransactionModel>(transaction));

                        }
                    }

                }

            }

            return recurringTransactionModels;

        }

        public RecurringTransactionModel Update(int id, JsonPatchDocument<RecurringTransactionUpdateModel> recurringTransactionPatch)
        {
            var recurringTransactionEntity = context.RecurringTransactions.Where(r => r.Id == id).FirstOrDefault();

            if (recurringTransactionEntity != null)
            {
                //TODO - test what happens if the patchDoc is invalid, i.e. contains a path / prop that does not exist
                var recurringTransactionToPatch = mapper.Map<RecurringTransactionUpdateModel>(recurringTransactionEntity);

                recurringTransactionPatch.ApplyTo(recurringTransactionToPatch);

                mapper.Map(recurringTransactionToPatch, recurringTransactionEntity);

                context.SaveChanges();

                return mapper.Map<RecurringTransactionModel>(recurringTransactionEntity);
            }

            return null;
        }

        XFinDbContext context;
        private readonly IMapper mapper;
        private readonly ITransactionService calculator;

        private bool TransactionIsDue(RecurringTransaction transaction, DateTime dueDate)
        {
            //TODO - check if this (DateTime) thing works....
            return transaction.EndDate != null
                ? DateTime.Compare(transaction.StartDate.Date, dueDate) <= 0 && DateTime.Compare(dueDate, (DateTime)transaction.EndDate?.Date) <= 0
                : DateTime.Compare(transaction.StartDate.Date, dueDate) <= 0;
        }
    }
}