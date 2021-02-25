using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOR.Pizzeria
{
    public class Kitchen
    {
        public List<Recipe> RecipeBook { get; set; }

        public async Task<Pizza>  PreparePizza(MenuItem menuItem)
        {
            Log.Information($"Started Preparing Pizza {menuItem.Name}");
            Console.WriteLine("Preparing " + menuItem.Name + "...");

            var recipe = RecipeBook.FirstOrDefault(x => string.Equals(x.Name.Trim(), menuItem.Name.Trim(), StringComparison.InvariantCultureIgnoreCase));

            var newPizza = new Pizza
            {
                Name = recipe.Name,
                BasePrice = menuItem.BasePrice,
            };
            Console.Write("Adding ");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.Write(ingredient + " ");
                newPizza.Toppings.Add(ingredient);
            }
            Console.Write($"to {newPizza.Name}");
            Console.WriteLine();
            await Bake(newPizza, recipe);
            Cut(newPizza, recipe);
            Box(newPizza);

            Log.Information($"Finished Preparing Pizza {menuItem.Name}");
            return newPizza;
        }

        public async Task Bake(Pizza pizza, Recipe recipe)
        {
            Log.Information($"Bakeing Pizza {pizza.Name}");
            
            foreach (var bakeingInstruction in recipe.BakeingSteps)
            {
                Console.WriteLine($"Baking {pizza.Name} for {bakeingInstruction.Time} minutes at {bakeingInstruction.Temperature} degrees...");
                // Fake waiting for the pizza to cook in the oven. 
                await Task.Delay(bakeingInstruction.Time * 10);                
            }
        }

        public void Cut(Pizza pizza, Recipe recipe)
        {
            Log.Information($"Cutting Pizza {pizza.Name}");
            Console.WriteLine($"Cutting {pizza.Name} into {recipe.Slices} slices");
            pizza.Slices = recipe.Slices;
        }

        public void Box(Pizza pizza)
        {
            Log.Information($"Boxing Pizza {pizza.Name}");
            Console.WriteLine($"Putting {pizza.Name} into a nice box...");
        }
    }
}
