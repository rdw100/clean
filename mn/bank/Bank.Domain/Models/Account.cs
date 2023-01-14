using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
    }
}
