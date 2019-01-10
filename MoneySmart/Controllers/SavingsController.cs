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

            var result = Mapper.Map<IEnumerable<SavingAccountWithoutTransactionsDto>>(savingAccountList);

            return Ok(result);
        }
        
        [HttpGet("{accountId}",Name = "getSavingAccount")]
        public IActionResult GetSavingAccount(int accountId)
        {
            SavingAccount savingAccount = _repository.GetSavingAccount(accountId, true);// true = with Transaction details

            if (savingAccount == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<SavingAccountDto>(savingAccount);

            return Ok(result);
        }

        [HttpGet("total")]
        public IActionResult GetTotalSavings()
        {
            var result = _repository.GetTotalSavings();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateSavingAccount([FromBody] SavingAccountCreateDto account)
        {
            if(account == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if Account already exists
            List<SavingAccount> savingAccounts = _repository.GetSavingAccounts(false);
            if(savingAccounts.FirstOrDefault(s => s.Name.Contains(account.Name,StringComparison.OrdinalIgnoreCase)) != null){
                return BadRequest(new { ErrorMsage = "This Saving Account name already exists." });
            }

            var accountEntity = Mapper.Map<SavingAccount>(account);
            _repository.AddSavingAccount(accountEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happend when creating the account. Please try again.");
            }

            var createdSavingAccount = Mapper.Map<SavingAccountDto>(accountEntity);

            return CreatedAtRoute("getSavingAccount",
                                   new { accountId = createdSavingAccount.Id },
                                   createdSavingAccount);
        }

        [HttpPost("{funds}")]
        public IActionResult AddFunds(double funds)
        {            

            return Ok();
        }
    }
}
