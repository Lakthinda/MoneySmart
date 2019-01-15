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
        private MoneySmartDbContext _context;
        public SavingsRepository(MoneySmartDbContext context)
        {            
            _context = context;            
        }
        
        public double GetTotalSavings()
        {
            // GetSavingsAccounts(true) = Savings with Transactions
            return GetSavingAccounts(true).Sum(s => s.Transactions.Sum(t => t.Amount));
        }

        public SavingAccount GetSavingAccount(int accountId, bool includeTransactions)
        {
            if (includeTransactions)
            {
                return _context.SavingAccounts.Include(a => a.Transactions).Where(a => a.Id == accountId).FirstOrDefault();
            }

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

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool SavingAccountExits(int accountId)
        {
            var savingAccount = GetSavingAccount(accountId, false);
            return savingAccount != null;
        }

        public void AddSavingAccount(SavingAccount account)
        {
            _context.SavingAccounts.Add(account);
        }

        public void RemoveSavingAccount(SavingAccount account)
        {
            _context.SavingAccounts.Remove(account);
        }
                
        public void AddTransactions(List<Transaction> transactions)
        {
            _context.Transactions.AddRange(transactions);
        }

        public void AddFund(double amount, int accountId = 0)
        {
            /*
             If AccountId != empty
              1. Get the saving account
                a. calculate the amount (amount * savings Account split %)
                b. Create a transaction
                c. Add transaction
             
            ELSE
              1. Get all savings accounts
              2. For each account, 
                a. calculate the amount (amount * savings Account split %)
                b. Create a transaction
                c. Add transaction
            */
            var transactionList = new List<Transaction>();

            if (accountId != 0)
            {
                SavingAccount account = GetSavingAccount(accountId, false);
                                
                transactionList.Add(new Transaction()
                {
                    Amount = amount,
                    OriginalAmount = amount,
                    SavingAccount = account,
                    SavingAccountId = account.Id,
                    TransactionType = 1, // 0 - split, 1 - adhoc
                    CreatedDateTime = DateTime.Now
                });              
            }
            else
            {
                List<SavingAccount> savingAccountList = GetSavingAccounts(false);
                
                foreach (var account in savingAccountList)
                {
                    if (!account.OnHold)
                    {
                        transactionList.Add(new Transaction()
                        {
                            Amount = amount * (account.Percentage / 100),
                            OriginalAmount = amount,
                            SavingAccount = account,
                            SavingAccountId = account.Id,
                            TransactionType = 0, // 0 - split, 1 - adhoc
                            CreatedDateTime = DateTime.Now
                        });
                    }
                }
            }

            AddTransactions(transactionList);
        }
    }
}
