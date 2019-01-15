using MoneySmart.API.Entities;
using System.Collections.Generic;


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

        void AddTransactions(List<Transaction> transactions);
        void AddFund(double amount, int accountId = 0);
    }
}