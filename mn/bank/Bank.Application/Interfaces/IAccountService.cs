using Bank.Application.ViewModels;
using Bank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Interfaces
{
    public interface IAccountService
    {
        AccountViewModel GetAccounts();
    }
}
