﻿using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoneySmart.API.Entities;
using MoneySmart.API.Models;
using MoneySmart.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneySmart.API.Controllers
{
    [EnableCors("AllowMyOrigin")]
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
                        
            
            var result =  Mapper.Map<IEnumerable<SavingAccountDto>>(savingAccountList);
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
        public IActionResult CreateSavingAccount(
                                                    [FromBody] SavingAccountCreateDto account)
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

        [HttpDelete("{accountId}")]
        public IActionResult DeleteSavingAccount(int accountId)
        {
            if (!_repository.SavingAccountExits(accountId))
            {
                return NotFound();
            }

            SavingAccount account = _repository.GetSavingAccount(accountId, false);
            _repository.RemoveSavingAccount(account);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happend when deleting saving account");
            }

            return NoContent();
        }

        [HttpPost("transactions/")]
        public IActionResult AddFunds(
                                        [FromBody] TransactionCreateDto transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (transaction.SavingAccountId != 0 && !_repository.SavingAccountExits(transaction.SavingAccountId))
            {
                return NotFound();
            }
                        
            if(transaction.SavingAccountId != 0)
            {
                // AdHoc Transaction, add funds to Saving account
                _repository.AddFund(transaction.Amount, transaction.SavingAccountId);                                
            }
            else
            {
                _repository.AddFund(transaction.Amount);

            }

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem happend when adding funds. Please try again.");
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPatch("{accountId}")]
        public IActionResult PartiallyUpdateSavingAccount(int accountId,
                                                          [FromBody] JsonPatchDocument<SavingAccountUpdateDto> patchDoc) 
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.SavingAccountExits(accountId))
            {
                return NotFound();
            }

            SavingAccount account = _repository.GetSavingAccount(accountId, false);
            if(account == null)
            {
                return BadRequest(new { ErrorMessage = "This Saving account does not exists" });
            }

            SavingAccountUpdateDto savingAccountToPatch = Mapper.Map<SavingAccountUpdateDto>(account);
            // Apply Patch
            patchDoc.ApplyTo(savingAccountToPatch, ModelState);
            // Validate Patch
            TryValidateModel(savingAccountToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // Map to SavingAccount Entity
            Mapper.Map(savingAccountToPatch, account);

            if (!_repository.Save())
            {
                return StatusCode(500, "A Problem happened when patch updating the saving account, please try again");
            }

            return NoContent() ;
        }
    }
}
