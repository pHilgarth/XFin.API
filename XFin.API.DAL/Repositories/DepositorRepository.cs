using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XFin.API.Core.Entities;
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
        public List<Depositor> GetDepositors(bool includeAccounts)
        {
            var depositors = context.Depositors.Include(d => d.BankAccounts).ToList();

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
