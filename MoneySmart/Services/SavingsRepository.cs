using MoneySmart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Services
{
    public class SavingsRepository
    {
        private SavingsDataStore _context;
        public SavingsRepository()
        {
            _context = SavingsDataStore.Current;
        }

        public List<SavingAccountWithoutTransactionsDto> GetSavingAccountsWithoutTransactions()
        {
            var savingAccounts = _context.Savings.Select(s => new SavingAccountWithoutTransactionsDto()
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
            var total = GetSavingAccountsWithoutTransactions().Sum(s => s.TotalSavings);

            return total;
        }

        public SavingAccountDto GetSavingAccount(int accountId)
        {
            var savingAccount = _context.Savings.FirstOrDefault(s => s.Id == accountId);

            return savingAccount;
        }

        public List<SavingAccountDto> GetSavingAccounts()
        {
            var savingAccounts = _context.Savings;

            return savingAccounts;
        }

        
    }
}
