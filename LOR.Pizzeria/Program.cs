using System;
using System.Collections.Generic;
using System.Linq;

namespace LOR.Pizzeria
{
    class Program
    {
        static void Main(string[] args)
        {

            var stores = LoadStores();

            var selectedStore = GetUsersLocation(stores);

            var selectedPizza = GetUsersPizza(selectedStore);

            selectedPizza.Prepare();
            selectedPizza.Bake();
            selectedPizza.Cut();
            selectedPizza.Box();
            selectedPizza.PrintReceipt();

            Console.WriteLine("\nYour pizza is ready!");
        }

        private static List<Store> LoadStores()
        {
            return new List<Store>
            {
                new Store
                {
                    Location = "Brisbane",
                    Menu = new List<Pizza> {
                        new Pizza() { Name = "Capriciosa", Ingredients = new List<string> { "mushrooms", "cheese", "ham", "mozarella" }, BasePrice = 20 },
                        new Pizza() { Name = "Florenza", Ingredients = new List<string> { "olives", "pastrami", "mozarella", "onion" }, BasePrice = 21},
                        new Pizza() { Name = "Margherita", Ingredients = new List<string> { "mozarella", "onion", "garlic", "oregano" }, BasePrice = 22}
                    }
                },
                new Store
                {
                    Location = "Sydney",
                    Menu = new List<Pizza> {
                        new Pizza() { Name = "Capriciosa", Ingredients = new List<string> { "mushrooms", "cheese", "ham", "mozarella" }, BasePrice = 30 },
                        new Pizza() { Name = "Inferno", Ingredients = new List<string> { "chili peppers", "mozzarella", "chicken", "cheese" }, BasePrice = 31}
                    }
                },
            };
        }

        private static Store GetUsersLocation(List<Store> stores)
        {
            var allStoreNames = String.Join(" or ", stores.Select(x => x.Location));
            Console.WriteLine($"Welcome to LOR Pizzeria! Please select the store location: {allStoreNames}");
            var Store = Console.ReadLine();
            var selectedStore = stores.FirstOrDefault(x => String.Equals(x.Location.Trim(), Store.Trim(), StringComparison.InvariantCultureIgnoreCase));

            while (selectedStore == null)
            {
                Console.WriteLine("Im Sorry, I don't recognize that location. Please select from the following store locations");
                Console.WriteLine($"{allStoreNames}");
                Store = Console.ReadLine();
                selectedStore = stores.FirstOrDefault(x => String.Equals(x.Location.Trim(), Store.Trim(), StringComparison.InvariantCultureIgnoreCase));
            }

            return selectedStore;
        }

        private static Pizza GetUsersPizza(Store store)
        {
            Console.WriteLine("MENU");
            foreach (var pizza in store.Menu)
            {                
                Console.WriteLine(pizza);
            }

            Console.WriteLine("What can I get you?");
            var pizzaType = Console.ReadLine();

            var selectedPizza = store.Menu.FirstOrDefault(x => String.Equals(x.Name.Trim(), pizzaType.Trim(), StringComparison.InvariantCultureIgnoreCase));
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

            return selectedPizza;
        }
    }
}
