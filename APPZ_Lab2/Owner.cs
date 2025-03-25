using APPZ_Lab2;
using System;
using System.Collections.Generic;

namespace APPZ_Lab2
{

    // Клас, що представляє хазяїна. Він може мати декілька тварин і
    // отримувати сповіщення про зміну їхнього стану (Observer Pattern).
 
    public class Owner
    {
        public string Name { get; }
        private List<Animal> animals;

        public Owner(string name)
        {
            Name = name;
            animals = new List<Animal>();
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public IEnumerable<Animal> Animals => animals;

        // Обробник подій зміни стану тварин.
        // Тепер він є public, щоб його можна було підписати напряму з Program.
  
        public void AnimalStateChangedHandler(object sender, AnimalEventArgs e)
        {
            // Тут симулюємо отримання сповіщення (наприклад, SMS)
            Console.WriteLine($"[Сповіщення для {Name}]: {e.Message}");
        }

        // Допоміжні методи для роботи з усіма тваринами (за бажанням).
        public void FeedAllAnimals(int simulationTime)
        {
            foreach (var animal in animals)
            {
                animal.Eat(simulationTime);
            }
        }

        public void CleanAllAnimals(int simulationTime)
        {
            foreach (var animal in animals)
            {
                animal.Clean(simulationTime);
            }
        }
    }
}
