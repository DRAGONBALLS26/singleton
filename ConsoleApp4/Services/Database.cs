using DrinkShop.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DrinkShop.Services
{
    public class Database
    {
        private Database() { }//constructor privat care se asigura ca nu creem mai multe instante

        //pseuda baza de date pe baza sablonului de proiectare singleton 
        //utilizand acest sablon ne asiguram pca pe durata de viata a programului se va crea doar o singura
        //instanta a acesteia 
        private static Database _instance = null;
        private static readonly object _lock = new object();
        private static readonly string dbPath = @"database.txt";

        public List<Drink> DrinkDb { get; set; }
        public static Database GetInstance() //metoda care ne returneaza instanta existenta sau creaza una noua daca nu a fost creata
        {
            if (_instance == null)
            {
                lock (_lock) //lock pentru a impiedica crearea de mai multe instante in programarea multi Threading
                {
                    if (_instance == null)
                    {
                        if (!File.Exists(dbPath))
                        {
                            using (var newFile = File.Create(dbPath)) { }
                            _instance = new Database
                            {
                                DrinkDb = new List<Drink>()
                            };
                        }
                        else
                        {
                            _instance = new Database
                            {
                                DrinkDb = new List<Drink>()
                            };
                            if (new FileInfo("database.txt").Length != 0)
                            {
                                var jsonString = File.ReadAllText(dbPath);
                                _instance.DrinkDb = JsonSerializer.Deserialize<List<Drink>>(jsonString);
                            }
                        }
                    }
                }
            }
            return _instance;
        }

        public void SaveData()
        {
            var jsonString = JsonSerializer.Serialize(DrinkDb);
            File.WriteAllText(dbPath, jsonString);
        }
    }
}
