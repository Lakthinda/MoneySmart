using MoneySmart.API.Entities;
using MoneySmart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Services
{
    public interface ISavingsRepository
    {        
        double GetTotalSavings();
        SavingAccount GetSavingAccount(int accountId,bool includeTransactions);
        List<SavingAccount> GetSavingAccounts(bool includeTransactions);
        bool Save();
        bool SavingAccountExits(int accountId);
        void AddSavingAccount(SavingAccount account);

        void RemoveSavingAccount(SavingAccount account);
        //void AddTransaction
    }
}
