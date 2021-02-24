using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LOR.Pizzeria
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()                
                .WriteTo.File("logs/Pizzeria.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            Log.Information("Pizzeria Application Started");

            var stores = LoadStores();
           
            var selectedStore = GetUsersLocation(stores);

            var selectedPizza = GetUsersPizza(selectedStore);

            selectedPizza.Prepare();
            selectedPizza.Bake();
            selectedPizza.Cut();
            selectedPizza.Box();
            selectedPizza.PrintReceipt();

            Console.WriteLine("\nYour pizza is ready!");
            Log.Information("Pizzeria Application Finished");
        }

        private static List<Store> LoadStores()
        {
            Log.Information("Load Stores Started");
            var stores = new List<Store>();
            var filePath = "Stores.json";
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
            Log.Information($"Load Stores Finished. {stores.Count} stores Loaded");
            return stores;
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

        private static Pizza GetUsersPizza(Store store)
        {
            Log.Information("Get User's pizza started");
            Console.WriteLine("MENU");
            foreach (var pizza in store.Menu)
            {                
                Console.WriteLine(pizza);
            }

            Console.WriteLine("What can I get you?");
            var pizzaType = Console.ReadLine();            
            var selectedPizza = store.Menu.FirstOrDefault(x => String.Equals(x.Name.Trim(), pizzaType.Trim(), StringComparison.InvariantCultureIgnoreCase));

            //Keep asking the user for a pizza until they enter one from the menu
            while (selectedPizza == null)
            {
                Console.WriteLine("Im Sorry, I don't recognize that Pizza. Please select from the following Pizzas");
                foreach (var pizza in store.Menu)
                {
                    Console.WriteLine(pizza);
                }

                pizzaType = Console.ReadLine();
                selectedPizza = store.Menu.FirstOrDefault(x => String.Equals(x.Name.Trim(), pizzaType.Trim(), StringComparison.InvariantCultureIgnoreCase));
            }

            Log.Information($"Get User's Pizza Finished. {selectedPizza} Selected");
            return selectedPizza;
        }
    }
}
