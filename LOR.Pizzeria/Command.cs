using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOR.Pizzeria
{
    public abstract class Command
    {
        public abstract Task<Pizza> Execute();
    }
}
