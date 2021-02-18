using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
using XFin.API.Core.Models;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Repositories
{
    public class DepositorRepository : IDepositorRepository
    {
        /*************************************************************************************************************
         * 
         * Constructors
         * 
        *************************************************************************************************************/
        public DepositorRepository(XFinDbContext context)
        {
            this.context = context;
        }

        /*************************************************************************************************************
         * 
         * Public Members
         * 
        *************************************************************************************************************/
        public List<DepositorModel> GetDepositors(bool includeAccounts)
        {
            var depositors = new List<DepositorModel>();

            foreach (var depositor in context.Depositors.Include(d => d.BankAccounts))
            {
                var depositorModel = new DepositorModel
                {
                    Id              = depositor.Id,
                    Name            = depositor.Name,
                    BankAccounts    = new List<BankAccountModel>()
                };

                if (includeAccounts)
                {
                    foreach (var bankAccount in depositor.BankAccounts)
                    {
                        depositorModel.BankAccounts.Add(
                            new BankAccountModel
                            {
                                Id              = bankAccount.Id,
                                DepositorId     = bankAccount.DepositorId,
                                Balance         = bankAccount.Balance,
                                AccountNumber   = bankAccount.AccountNumber,
                                Iban            = bankAccount.Iban,
                                Bic             = bankAccount.Bic,
                                Bank            = bankAccount.Bank,
                                AccountType     = bankAccount.AccountType
                            });
                    }
                }

                depositors.Add(depositorModel);

            }

            return depositors.Count != 0 ? depositors : null;
        }

        public DepositorModel GetDepositor(int id, bool includeAccounts)
        {
            var depositor = context.Depositors.Where(d => d.Id == id).Include(d => d.BankAccounts).FirstOrDefault();

            if (depositor != null)
            {
                var depositorModel = new DepositorModel
                {
                    Id = depositor.Id,
                    Name = depositor.Name,
                    BankAccounts = new List<BankAccountModel>()
                };

                if (includeAccounts)
                {
                    foreach (var bankAccount in depositor.BankAccounts)
                    {
                        depositorModel.BankAccounts.Add(
                            new BankAccountModel
                            {
                                Id = bankAccount.Id,
                                DepositorId = bankAccount.DepositorId,
                                Balance = bankAccount.Balance,
                                AccountNumber = bankAccount.AccountNumber,
                                Iban = bankAccount.Iban,
                                Bic = bankAccount.Bic,
                                Bank = bankAccount.Bank,
                                AccountType = bankAccount.AccountType
                            });
                    }
                }

                return depositorModel;
            }
            else
            {
                return null;
            }
        }

        /*************************************************************************************************************
         * 
         * Private Members
         * 
        *************************************************************************************************************/
        private readonly XFinDbContext context;

    }
}
