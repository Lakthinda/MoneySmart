using MoneySmart.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Services
{
    public class SavingsManagerService : ISavingsManager
    {
        private ISavingsRepository _repository;
        public SavingsManagerService(ISavingsRepository repository)
        {
            _repository = repository;
        }
       public bool AddFund(double amount, int accountId = 0)
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
            
            if (accountId != 0)
            {
                SavingAccount account = _repository.GetSavingAccount(accountId, false);
                
                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                                    {
                                        Amount = amount,
                                        OriginalAmount = amount,
                                        SavingAccount = account,
                                        SavingAccountId = account.Id,
                                        TransactionType = 1, // 0 - split, 1 - adhoc
                                        CreatedDateTime = DateTime.Now
                                    });

                _repository.AddTransactions(transactionList);
                return _repository.Save();
            }else
            {
                List<SavingAccount> savingAccountList = _repository.GetSavingAccounts(false);
                List<Transaction> transactionList = new List<Transaction>();

                foreach(var account in savingAccountList)
                {
                    if (!account.OnHold)
                    {
                        transactionList.Add(new Transaction()
                        {
                            Amount = amount * account.Percentage,
                            OriginalAmount = amount,
                            SavingAccount = account,
                            SavingAccountId = account.Id,
                            TransactionType = 0, // 0 - split, 1 - adhoc
                            CreatedDateTime = DateTime.Now
                        });
                    }
                }

                _repository.AddTransactions(transactionList);
                return _repository.Save();
            }

        }
    }
}
