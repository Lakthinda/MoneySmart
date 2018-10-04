using Microsoft.AspNetCore.Mvc;
using MoneySmart.API.Models;
using MoneySmart.API.Services;
using System;
using System.Collections.Generic;

namespace MoneySmart.API.Controllers
{
    [Route("api/savings")]
    public class SavingsController : Controller
    {

        private SavingsRepository _repository;
        public SavingsController(SavingsRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet()]
        //public IActionResult GetSavings()
        //{
        //    var result = 1000;
        //    return Ok(result);
        //}

        [HttpGet()]
        public IActionResult GetSavingAccounts()
        {
            var result = _repository.GetSavingAccountsWithoutTransactions();
            return Ok(result);
        }

        [HttpGet("total")]
        public IActionResult GetTotalSavings()
        {
            var result = _repository.GetTotalSavings();
            return Ok(result);
        }

        [HttpGet("{accountId}")]
        public IActionResult GetSavingAccount(int accountId)
        {
            var result = _repository.GetSavingAccount(accountId);
            return Ok(result);
        }        

        [HttpPost("{funds}")]
        public IActionResult AddFunds(double funds)
        {            
            var savingAccounts = _repository.GetSavingAccounts();

            foreach (var account in savingAccounts)
            {                
                account.Transactions.Add(new TransactionDto()
                {
                    Id= 1201,
                    Amount = (account.Percentage/100) * funds,
                    TransactionType = 0,
                    CreatedDateTime = DateTime.Now
                });
            }

            return Ok(savingAccounts);
        }
    }
}
