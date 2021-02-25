using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public class FrontOfHouse
    {
        public Pizza RunCommand(Command command)
        {
            Log.Information($"Sending command from front of house");
            return command.Execute();
        }
    }
}
