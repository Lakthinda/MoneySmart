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
        List<SavingAccountWithoutTransactionsDto> GetSavingAccountsWithoutTransactions();
        double GetTotalSavings();
        SavingAccount GetSavingAccount(int accountId);
        List<SavingAccount> GetSavingAccounts();


    }
}
