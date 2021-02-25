using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOR.Pizzeria
{
    public class FrontOfHouse
    {
        public Task<Pizza> RunCommand(Command command)
        {
            Log.Information($"Sending command from front of house");
            return command.Execute();
        }
    }
}
