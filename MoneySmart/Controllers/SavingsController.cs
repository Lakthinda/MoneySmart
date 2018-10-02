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

        //private SavingsRepository _repository;
        //public SavingsController(SavingsRepository repository)
        //{
        //    _repository = repository;
        //}

        [HttpGet()]
        public IActionResult GetSavings()
        {
            var result = 1000;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSavings(int id)
        {
            var result = (id==0)? 500 : id ;
            return Ok(result);
        }
        
        [HttpPost("{funds}")]
        public IActionResult AddFunds(double funds)
        {
            SavingsRepository _repository = new SavingsRepository();
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
