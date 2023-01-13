using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Models
{
    public class Account
    {
        public int Number { get; }
        public string Owner { get; set; }
        public decimal Balance { get; }

        public Account(string name, decimal initialBalance)
        {
            Owner = name;
            Balance = initialBalance;
        }
    }
}
