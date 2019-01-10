﻿using MoneySmart.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MoneySmart.API.Services
{
    public interface ISavingsManager
    {
        IEnumerable<Transaction> CreateTransaction();
    }
}