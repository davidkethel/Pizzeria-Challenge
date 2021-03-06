﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOR.Pizzeria
{

    class MakePizzaCommand : Command
    {
        private Kitchen Kitchen;

        private MenuItem MenuItem;

        public MakePizzaCommand(Kitchen kitchen, MenuItem menuItem)
        {
            Kitchen = kitchen;
            MenuItem = menuItem;
        }

        public override Task<Pizza> Execute()
        {
            Log.Information($"Executing Make Pizza Command : {MenuItem.Name}");
            return Kitchen.PreparePizza(MenuItem);
        }
    }
}
