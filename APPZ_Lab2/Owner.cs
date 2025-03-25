using System;
using System.Collections.Generic;

namespace APPZ_Lab2
{
    /// <summary>
    /// Клас, що представляє власника.
    /// Власник може мати декілька тварин і отримувати повідомлення (через події) про зміну їхнього стану.
    /// </summary>
    public class Owner
    {
        public string Name { get; }
        private List<Animal> animals;

        public IEnumerable<Animal> Animals => animals;

        public Owner(string name)
        {
            Name = name;
            animals = new List<Animal>();
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public void AnimalStateChangedHandler(object sender, AnimalEventArgs e)
        {
            // Сповіщення власника про зміну стану тварини
            Console.WriteLine($"[Сповіщення для власника {Name}]: {e.Message}");
        }

        public void FeedAllAnimals(int simulationTime)
        {
            foreach (var animal in animals)
                animal.Eat(simulationTime);
        }

        public void CleanAllAnimals(int simulationTime)
        {
            foreach (var animal in animals)
                animal.Clean(simulationTime);
        }
    }
}
