using MoneySmart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySmart.API.Services
{
    public class SavingsRepository
    {
        private List<SavingAccountDto> _repository;
        public SavingsRepository()
        {
            _repository = GetRepository();
        }

        public List<SavingAccountDto> GetSavingAccounts()
        {
            return GetRepository();
        }

        #region PrivateRepo-ToBe Updated
        private List<SavingAccountDto> GetRepository()
        {
            return new List<SavingAccountDto>()
                    {
                        new SavingAccountDto()
                        {
                            Id = 101,
                            Name = "Primary Savings",
                            Description = "Primary Savings",
                            Percentage = 13,
                            IsPrimary = true,
                            OnHold = false,
                            Transactions = new List<TransactionDto>()
                            {
                                new TransactionDto()
                                {
                                    Id = 1001,
                                    Amount = 100,
                                    TransactionType = 0,
                                    CreatedDateTime = DateTime.Now
                                },
                                new TransactionDto()
                                {
                                    Id = 1002,
                                    Amount = 500,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now
                                },
                                new TransactionDto()
                                {
                                    Id = 1003,
                                    Amount = 200,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now
                                }
                            }
                        },
                        new SavingAccountDto()
                        {
                            Id = 101,
                            Name = "House",
                            Description = "",
                            Percentage = 39,
                            IsPrimary = true,
                            OnHold = false,
                            Transactions = new List<TransactionDto>()
                            {
                                new TransactionDto()
                                {
                                    Id = 1001,
                                    Amount = 1100,
                                    TransactionType = 0,
                                    CreatedDateTime = DateTime.Now
                                },
                                new TransactionDto()
                                {
                                    Id = 1002,
                                    Amount = 5100,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now
                                },
                                new TransactionDto()
                                {
                                    Id = 1003,
                                    Amount = 2100,
                                    TransactionType = 1,
                                    CreatedDateTime = DateTime.Now
                                }
                            }
                        }
                    };
        }

        #endregion
    }
}
