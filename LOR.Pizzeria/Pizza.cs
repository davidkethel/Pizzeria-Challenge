using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public class Pizza
    {
       
        public string Name { get; set; }
        public List<string> Toppings { get; set; } = new List<string>();
        public decimal BasePrice { get; set; }
        public int Slices { get; set; }
        
        public void PrintReceipt()
        {
            Console.WriteLine($"Total price: { BasePrice}");
        }
    }
}
