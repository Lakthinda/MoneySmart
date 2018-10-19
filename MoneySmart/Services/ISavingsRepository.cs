using MoneySmart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Services
{
    interface ISavingsRepository
    {
        List<SavingAccountWithoutTransactionsDto> GetSavingAccountsWithoutTransactions();
        double GetTotalSavings();
        SavingAccountDto GetSavingAccount(int accountId);
        List<SavingAccountDto> GetSavingAccounts();


    }
}
