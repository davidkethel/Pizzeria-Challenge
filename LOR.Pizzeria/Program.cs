using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LOR.Pizzeria
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fudge to get async to work in a console app
            Task t = MainAsync(args);
            t.Wait();
        }

        static async Task MainAsync(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/Pizzeria.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            Log.Information("Pizzeria Application Started");

            var recipes = LoadRecipes();
            var stores = LoadStores(recipes);

            var selectedStore = GetUsersLocation(stores);
            var order = GetUsersOrder(selectedStore, recipes);            

            var pizzas = new List<Pizza>();

            var makePizzaTasks = new List<Task<Pizza>>();

            foreach (var menuItem in order)
            {
                // Create a new "Create Pizza Command" for every item on the order. 
                var makePizzaCommand = new MakePizzaCommand(selectedStore.kitchen, menuItem);

                // Get the front of house team to issue the command. 
                makePizzaTasks.Add(selectedStore.frontOfHouse.RunCommand(makePizzaCommand));
            }

            // Wait for all the pizzas to be made
            await Task.WhenAll(makePizzaTasks);            

            Console.WriteLine("\nYour order is ready!");
            Console.WriteLine($"Total Price {makePizzaTasks.Sum(x => x.Result.BasePrice)}");

            Log.Information("Pizzeria Application Finished");
        }

        private static List<Store> LoadStores(List<Recipe> recipes)
        {
            Log.Information("Load Stores Started");
            var stores = new List<Store>();
            var filePath = @"Data\Stores.json";
            try
            {
                var jsonString = File.ReadAllText(filePath);
                stores = JsonSerializer.Deserialize<List<Store>>(jsonString);
            }
            catch (Exception)
            {
                var errorMsg = $"ERROR reading data from file: {filePath}";
                Console.Error.WriteLine(errorMsg);
                Log.Error(errorMsg);
                Environment.Exit(0);
            }

            //ensure stores only include menu items we have a recipe for
            foreach (var store in stores)
            {
                var missingRecipes = store.Menu.Select(x => x.Name).Except(recipes.Select(r => r.Name)).ToList();
                if (missingRecipes.Any())
                {
                    foreach (var missingRecipe in missingRecipes)
                    {
                        Log.Warning($"WARNING : No recipe found for menu item { missingRecipe } in location {store.Location}");
                        Log.Warning($"WARNING : Menu Item { missingRecipe } will be removed from {store.Location}");
                        store.Menu.RemoveAll(x => x.Name == missingRecipe);
                    }
                }

                // Give each kitchen a recipe book. 
                store.kitchen.RecipeBook = recipes;
            }

            Log.Information($"Load Stores Finished. {stores.Count} stores Loaded");
            return stores;
        }


        private static List<Recipe> LoadRecipes()
        {
            Log.Information("Load Recipes Started");
            var recipes = new List<Recipe>();
            var filePath = @"Data\Recipes.json";
            try
            {
                var jsonString = File.ReadAllText(filePath);
                recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonString);
            }
            catch (Exception)
            {
                var errorMsg = $"ERROR reading data from file: {filePath}";
                Console.Error.WriteLine(errorMsg);
                Log.Error(errorMsg);
                Environment.Exit(0);
            }
            Log.Information($"Load Recipes Finished. {recipes.Count} recipes Loaded");
            return recipes;
        }

        private static Store GetUsersLocation(List<Store> stores)
        {
            Log.Information("Get User's Location Started");
            var allStoreNames = String.Join(" or ", stores.Select(x => x.Location));
            Console.WriteLine($"Welcome to LOR Pizzeria! Please select the store location: {allStoreNames}");
            var Store = Console.ReadLine();
            var selectedStore = stores.FirstOrDefault(x => String.Equals(x.Location.Trim(), Store.Trim(), StringComparison.InvariantCultureIgnoreCase));

            //Keep asking the user for their location until they enter one we recognise.
            while (selectedStore == null)
            {
                Console.WriteLine("Im Sorry, I don't recognize that location. Please select from the following store locations");
                Console.WriteLine($"{allStoreNames}");
                Store = Console.ReadLine();
                selectedStore = stores.FirstOrDefault(x => String.Equals(x.Location.Trim(), Store.Trim(), StringComparison.InvariantCultureIgnoreCase));
            }

            Log.Information($"Get User's Location Finished. {selectedStore.Location} Selected");
            return selectedStore;
        }

        private static List<MenuItem> GetUsersOrder(Store store, List<Recipe> recipes)
        {
            Log.Information("Get User's Menu choice started");
            Console.WriteLine("MENU");

            var order = new List<MenuItem>();
            var orderComplete = false;

            // Build up a dictionary with the name and description of each menu item.
            // That way if need be we don't need to rebuild the description text later on.
            var menuDesriptions = new Dictionary<string, string>();
            foreach (var menuItem in store.Menu)
            {                
                var Ingredients = recipes.FirstOrDefault(x => x.Name == menuItem.Name).Ingredients;
                var ingredientsList = string.Join(", ", Ingredients);
                var description = $"{menuItem.Name} - {ingredientsList} - {menuItem.BasePrice} AUD";

                menuDesriptions.Add(menuItem.Name, description);             
            }

            // Keep adding pizza's to the order untill the user says no.
            while (!orderComplete)
            {
                foreach (var menuItem in store.Menu)
                {
                    Console.WriteLine(menuDesriptions.GetValueOrDefault(menuItem.Name));
                }

                Console.WriteLine("What can I get you?");
                var menuType = Console.ReadLine();
                var selectedMenuItem = store.Menu.FirstOrDefault(x => string.Equals(x.Name.Trim(), menuType.Trim(), StringComparison.InvariantCultureIgnoreCase));

                //Keep asking the user for a Menu Item until they enter one from the menu
                while (selectedMenuItem == null)
                {
                    Console.WriteLine("Im Sorry, I don't recognize that dish. Please select from the following menu");
                    foreach (var menuItem in store.Menu)
                    {
                        Console.WriteLine(menuDesriptions.GetValueOrDefault(menuItem.Name));
                    }

                    menuType = Console.ReadLine();
                    selectedMenuItem = store.Menu.FirstOrDefault(x => string.Equals(x.Name.Trim(), menuType.Trim(), StringComparison.InvariantCultureIgnoreCase));
                }

                order.Add(selectedMenuItem);
                Console.WriteLine($"{selectedMenuItem.Name} added to your order.");                

                Console.WriteLine("Would you like to add another pizza to your order? [Y/N]");
                orderComplete = Console.ReadLine() == "N";

            }

            Log.Information($"Get User's Order Finished. {order.Count()} items ordered.");
            return order;
        }
    }
}
