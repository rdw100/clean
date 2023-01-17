using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Commands
{
    public class CreateAccountCommand : AccountCommand
    {
        public CreateAccountCommand(string owner, decimal balance) 
        { 
            Owner= owner;
            Balance= balance;   
        }
    }
}
