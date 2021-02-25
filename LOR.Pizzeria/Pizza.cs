using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public class Pizza
    {
        public Pizza(MenuItem selectedMenuItem, Recipe recipe)
        {
            Name = selectedMenuItem.Name;
            Ingredients = recipe.Ingredients;
            BasePrice = selectedMenuItem.BasePrice;
            Slices = recipe.Slices;
            BakeingInstructions = recipe.BakeingSteps;
        }

        public string Name { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public decimal BasePrice { get; set; }
        public int Slices { get; set; }
        public List<BakeingInstructions> BakeingInstructions { get; set; }


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
            foreach (var bakeingInstruction in BakeingInstructions)
            {
                Console.WriteLine($"Baking pizza for {bakeingInstruction.Time} minutes at {bakeingInstruction.Temperature} degrees...");
            }
        }

        public void Cut()
        {
            Console.WriteLine($"Cutting pizza into {Slices} slices");
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
