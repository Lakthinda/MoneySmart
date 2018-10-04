using MoneySmart.API.Models;
using System;
using System.Collections.Generic;

namespace MoneySmart.API
{
    public class SavingsDataStore
    {
        public SavingsDataStore()
        {
             Savings = new List<SavingAccountDto>()
                    {
                        new SavingAccountDto()
                        {
                            Id = 101,
                            Name = "Primary Savings",
                            Description = "Primary Savings",
                            Percentage = 80,
                            IsPrimary = true,
                            OnHold = false,
                            Transactions = new List<TransactionDto>()
                            {
                                new TransactionDto()
                                {
                                    Id = 1001,
                                    Amount = 100,
                                    TransactionType = 0,
                                    CreatedDateTime = DateTime.Now,
                                    OriginalAmount = 500
                                },
                                new TransactionDto()
                                {
                                    Id = 1002,
                                    Amount = 500,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now,
                                    OriginalAmount = 500
                                },
                                new TransactionDto()
                                {
                                    Id = 1003,
                                    Amount = 200,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now,
                                    OriginalAmount = 200
                                }
                            }
                        },
                        new SavingAccountDto()
                        {
                            Id = 102,
                            Name = "House",
                            Description = "",
                            Percentage = 20,
                            IsPrimary = false,
                            OnHold = false,
                            Transactions = new List<TransactionDto>()
                            {
                                new TransactionDto()
                                {
                                    Id = 1001,
                                    Amount = 1100,
                                    TransactionType = 0,
                                    CreatedDateTime = DateTime.Now,
                                    OriginalAmount = 2000
                                },
                                new TransactionDto()
                                {
                                    Id = 1002,
                                    Amount = 5100,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now,
                                    OriginalAmount = 5100
                                },
                                new TransactionDto()
                                {
                                    Id = 1003,
                                    Amount = 2100,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now,
                                    OriginalAmount = 2100
                                }
                            }
                        }
                    };
        }
        public static SavingsDataStore Current { get; } = new SavingsDataStore();

        public List<SavingAccountDto> Savings { get; set; }


    }
}
