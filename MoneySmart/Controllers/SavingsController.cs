using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneySmart.API.Entities;
using MoneySmart.API.Models;
using MoneySmart.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.API.Controllers
{
    [Route("api/savings")]
    public class SavingsController : Controller
    {

        private ISavingsRepository _repository;
        public SavingsController(ISavingsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetSavingAccounts()
        {
            List<SavingAccount> savingAccountList = _repository.GetSavingAccounts(true); //true = with transaction details

            //List<SavingAccountWithoutTransactionsDto> savingAccountWithoutTransactionsDtoList = new List<SavingAccountWithoutTransactionsDto>();
            //foreach(var account in savingAccountList)
            //{
            //    savingAccountWithoutTransactionsDtoList.Add(new SavingAccountWithoutTransactionsDto()
            //    {
            //        Id = account.Id,
            //        Name = account.Name,
            //        Description = account.Description,
            //        IsPrimary = account.IsPrimary,
            //        OnHold = account.OnHold,
            //        Percentage = account.Percentage,
            //        TotalSavings = account.Transactions.Sum(t => t.Amount)
            //    });
            //}

            var result = Mapper.Map<IEnumerable<SavingAccountWithoutTransactionsDto>>(savingAccountList);

            return Ok(result);
        }
        
        [HttpGet("{accountId}")]
        public IActionResult GetSavingAccount(int accountId)
        {
            SavingAccount savingAccount = _repository.GetSavingAccount(accountId, true);// true = with Transaction details

            //SavingAccountDto savingAccountDto = new SavingAccountDto()
            //{
            //    Id = savingAccount.Id,
            //    Name = savingAccount.Name,
            //    Description = savingAccount.Description,
            //    IsPrimary = savingAccount.IsPrimary,
            //    OnHold = savingAccount.OnHold,
            //    Percentage = savingAccount.Percentage,
            //    TotalSavings = savingAccount.Transactions.Sum(t => t.Amount),
            //    Transactions = savingAccount.Transactions.Select(a => new TransactionDto()
            //    {
            //        Id = a.Id,
            //        Amount = a.Amount,
            //        CreatedDateTime = a.CreatedDateTime,
            //        OriginalAmount = a.OriginalAmount,
            //        TransactionType = a.TransactionType
            //    }).ToList()

            //};

            var result = Mapper.Map<SavingAccountDto>(savingAccount);

            return Ok(result);
        }

        [HttpGet("total")]
        public IActionResult GetTotalSavings()
        {
            var result = _repository.GetTotalSavings();
            return Ok(result);
        }

        [HttpPost("{funds}")]
        public IActionResult AddFunds(double funds)
        {            
            //var savingAccounts = _repository.GetSavingAccounts();

            //foreach (var account in savingAccounts)
            //{                
            //    account.Transactions.Add(new TransactionDto()
            //    {
            //        Id= 1201,
            //        Amount = (account.Percentage/100) * funds,
            //        TransactionType = 0,
            //        CreatedDateTime = DateTime.Now
            //    });
            //}

            return Ok();
        }
    }
}
