using Microsoft.EntityFrameworkCore;
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

        //public List<SavingAccountWithoutTransactionsDto> GetSavingAccountsWithoutTransactions()
        //{
        //    var savingAccounts = _context.SavingAccounts.Select(s => new SavingAccountWithoutTransactionsDto()
        //    {
        //        Id = s.Id,
        //        Name = s.Name,
        //        Description = s.Description,
        //        IsPrimary = s.IsPrimary,
        //        OnHold = s.OnHold,
        //        Percentage = s.Percentage,
        //        TotalSavings = s.Transactions.Sum(t => t.Amount)
        //    }).ToList();


        //    return savingAccounts;
        //}

        public double GetTotalSavings()
        {
            //var total = GetSavingAccountsWithoutTransactions().Sum(s => s.TotalSavings);

            // GetSavingsAccounts with Transactions
            return GetSavingAccounts(true).Sum(s => s.Transactions.Sum(t => t.Amount));
        }

        public SavingAccount GetSavingAccount(int accountId, bool includeTransactions)
        {
            if (includeTransactions)
            {
                return _context.SavingAccounts.Include(a => a.Transactions).Where(a => a.Id == accountId).FirstOrDefault();
            }

            //var savingAccount = _context.SavingAccounts.FirstOrDefault(s => s.Id == accountId);

            return _context.SavingAccounts.Where(a => a.Id == accountId).FirstOrDefault();
        }

        public List<SavingAccount> GetSavingAccounts(bool includeTransactions)
        {

            if (includeTransactions)
            {
                return _context.SavingAccounts.Include(a => a.Transactions).OrderBy(a => a.Name).ToList();
            }

            return _context.SavingAccounts.OrderBy(a => a.Name).ToList();
        }

    }
}
