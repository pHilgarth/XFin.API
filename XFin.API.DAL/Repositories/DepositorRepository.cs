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

            if (includeAccounts)
            {
                foreach (var depositor in context.Depositors.Include(d => d.BankAccounts))
                {
                    var depositorModel = new DepositorModel
                    {
                        Id              = depositor.Id,
                        Name            = depositor.Name,
                        BankAccounts    = new List<BankAccountModel>()
                    };

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

                    depositors.Add(depositorModel);

                }

                return depositors;
            }
            else
            {
                foreach (var depositor in context.Depositors.ToList<Depositor>())
                {
                    depositors.Add(
                        new DepositorModel
                        {
                            Id      = depositor.Id,
                            Name    = depositor.Name
                        });
                }
            }

            return depositors;
        }
        /*************************************************************************************************************
         * 
         * Private Members
         * 
        *************************************************************************************************************/
        private readonly XFinDbContext context;
    }
}
