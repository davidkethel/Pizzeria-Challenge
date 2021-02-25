using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOR.Pizzeria
{
    public class Kitchen
    {

        public List<Recipe> RecipeBook { get; set; }

        public Pizza PreparePizza(MenuItem menuItem)
        {
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
            Bake(newPizza, recipe);
            Cut(newPizza, recipe);
            Box(newPizza);

            return newPizza;
        }

        public void Bake(Pizza pizza, Recipe recipe)
        {            
            foreach (var bakeingInstruction in recipe.BakeingSteps)
            {
                Console.WriteLine($"Baking {pizza.Name} for {bakeingInstruction.Time} minutes at {bakeingInstruction.Temperature} degrees...");
            }
        }

        public void Cut(Pizza pizza, Recipe recipe)
        {            
            Console.WriteLine($"Cutting {pizza.Name} into {recipe.Slices} slices");
            pizza.Slices = recipe.Slices;
        }

        public void Box(Pizza pizza)
        {
            Console.WriteLine($"Putting {pizza.Name} into a nice box...");
        }
    }
}
