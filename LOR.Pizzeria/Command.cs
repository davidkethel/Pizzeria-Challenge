using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public abstract class Command
    {
        public abstract Pizza Execute();
    }
}
