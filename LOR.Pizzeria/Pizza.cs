using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public class Pizza
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public decimal BasePrice { get; set; }

        public void Prepare()
        {
            Console.WriteLine("Preparing " + Name + "...");
            Console.Write("Adding ");
            foreach (var i in Ingredients)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        public void Bake()
        {
            if (Name == "Margherita")
                Console.WriteLine("Baking pizza for 15 minutes at 200 degrees...");
            else
            {
                Console.WriteLine("Baking pizza for 30 minutes at 200 degrees...");
            }
        }

        public void Cut()
        {
            if (Name == "Florenza")
                Console.WriteLine("Cutting pizza into 6 slices with a special knife...");
            else
            {
                Console.WriteLine("Cutting pizza into 8 slices...");
            }
        }

        public void Box()
        {
            Console.WriteLine("Putting pizza into a nice box...");
        }


        public void PrintReceipt()
        {
            Console.WriteLine("Total price: " + BasePrice);
        }
    }
}
