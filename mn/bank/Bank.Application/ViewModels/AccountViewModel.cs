using Bank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
