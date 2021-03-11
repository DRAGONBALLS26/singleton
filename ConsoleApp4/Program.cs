using DrinkShop.Models;
using DrinkShop.Services;
using System;

namespace Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var DatabaseBySingleton = Database.GetInstance(); //apelam instanta de Db
            bool status = true;
            while (status)
            {
                Console.WriteLine($"La moment aveti {DatabaseBySingleton.DrinkDb.Count} bauturi in baza de date.");
                foreach (var obj in DatabaseBySingleton.DrinkDb) //afisam obiectele din db
                {
                    Console.WriteLine($"Denumirea bauturii : {obj.Name}");
                }
                Console.WriteLine("Daca doriti sa adaugati o bautura nou tastati <D> "); //adaugam obiecte noi daca dorim
                var answer = Console.ReadLine();
                if (answer == "D")
                {
                    Console.Write("Introduceti denumirea bauturii dorite : ");
                    var newDrink = Console.ReadLine();
                    var drink = new Drink
                    {
                        Name = newDrink
                    };
                    DatabaseBySingleton.DrinkDb.Add(drink);
                    DatabaseBySingleton = Database.GetInstance();
                }
                else
                    status = false;
                Console.ReadKey();
            }
            








        }

    }
}
