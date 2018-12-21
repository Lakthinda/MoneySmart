using MoneySmart.API.Entities;
using MoneySmart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Services
{
    public class SavingsRepository : ISavingsRepository
    {
        //private SavingsDataStore _context;
        private MoneySmartDbContext _context;
        public SavingsRepository(MoneySmartDbContext context)
        {
            //_context = SavingsDataStore.Current;
            _context = context;            
        }

        public List<SavingAccountWithoutTransactionsDto> GetSavingAccountsWithoutTransactions()
        {
            var savingAccounts = _context.SavingAccounts.Select(s => new SavingAccountWithoutTransactionsDto()
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IsPrimary = s.IsPrimary,
                OnHold = s.OnHold,
                Percentage = s.Percentage,
                TotalSavings = s.Transactions.Sum(t => t.Amount)
            }).ToList();


            return savingAccounts;
        }

        public double GetTotalSavings()
        {
            //var total = GetSavingAccountsWithoutTransactions().Sum(s => s.TotalSavings);

            var total = 0;
            return total;
        }

        public SavingAccount GetSavingAccount(int accountId)
        {
            var savingAccount = _context.SavingAccounts.FirstOrDefault(s => s.Id == accountId);

            return savingAccount;
        }

        public List<SavingAccount> GetSavingAccounts()
        {
            var savingAccounts = _context.SavingAccounts.ToList();

            return savingAccounts;
        }

    }
}
