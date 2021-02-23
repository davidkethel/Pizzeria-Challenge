using System;
using System.Collections.Generic;

namespace LOR.Pizzeria
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to LOR Pizzeria! Please select the store location: Brisbane OR Sydney");
            var Store = Console.ReadLine();

            Console.WriteLine("MENU");
            if (Store == "Brisbane")
            {
                Console.WriteLine("Capriciosa - mushrooms, cheese, ham, mozarella - 20 AUD");
                Console.WriteLine("Florenza - olives, pastrami, mozarella, onion - 21 AUD");
                Console.WriteLine("Margherita - mozarella, onion, garlic, oregano - 22 AUD");
            }
            else if (Store == "Sydney")
            {
                Console.WriteLine("Capriciosa - mushrooms, cheese, ham, mozarella - 30 AUD");
                Console.WriteLine("Inferno - chili peppers, mozzarella, chicken, cheese - 31 AUD");
            }



            Console.WriteLine("What can I get you?");

            var pizzaType = Console.ReadLine();


            var pizza = new Pizza();
            switch(pizzaType)
            {
                case "Capriciosa":
                    var capriciosaPrice = 0;
                    if (Store == "Brisbane") capriciosaPrice = 20;
                    if (Store == "Sydney") capriciosaPrice = 30;

                    pizza = new Pizza(){ Name = "Capriciosa", Ingredients = new List<string>{ "mushrooms", "cheese", "ham", "mozarella" }, BasePrice = capriciosaPrice};
                    break;
                case "Florenza":
                    pizza = new Pizza() { Name = "Florenza", Ingredients = new List<string> { "olives", "pastrami", "mozarella", "onion" }, BasePrice = 21};
                    break;
                case "Margherita":
                    pizza = new Pizza() { Name = "Margherita", Ingredients = new List<string> { "mozarella", "onion", "garlic", "oregano" }, BasePrice = 22};
                    break;
                case "Inferno":
                    pizza = new Pizza() { Name = "Inferno", Ingredients = new List<string> { "chili peppers", "mozzarella", "chicken", "cheese" }, BasePrice = 31};
                    break;
                default:
                    break;
            }
            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();
            pizza.PrintReceipt();

            Console.WriteLine("\nYour pizza is ready!");
        }
    }

   
}
