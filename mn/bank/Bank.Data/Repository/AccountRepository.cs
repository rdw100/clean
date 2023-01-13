﻿using Bank.Data.Context;
using Bank.Domain.Interface;
using Bank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankDbContext _ctx;

        public AccountRepository(BankDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}
