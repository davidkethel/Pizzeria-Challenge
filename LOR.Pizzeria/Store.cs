using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LOR.Pizzeria
{

    public class Store
    {
        public string Location { get; set; }

        public List<MenuItem> Menu { get; set; }

        public Kitchen kitchen = new Kitchen();

        public FrontOfHouse frontOfHouse = new FrontOfHouse();

    }
}
