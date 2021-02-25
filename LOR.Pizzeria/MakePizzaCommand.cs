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

        public override Pizza Execute()
        {
            return Kitchen.PreparePizza(MenuItem);
        }
    }
}
