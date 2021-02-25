using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public class FrontOfHouse
    {
        public Pizza RunCommand(Command command)
        {
            return command.Execute();
        }
    }
}
