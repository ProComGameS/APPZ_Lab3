using System;
using System.Collections.Generic;

namespace APPZ_Lab2
{
    /// <summary>
    /// Клас, що представляє зоомагазин.
    /// Метод SellAnimal дозволяє забрати (продати) тварину із магазину.
    /// </summary>
    public class PetShop
    {
        public string Name { get; }
        private List<Animal> animals;

        public IEnumerable<Animal> Animals => animals;

        public PetShop(string name)
        {
            Name = name;
            animals = new List<Animal>();
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        
        // Забрати тварину з магазину за вказаним індексом.
       
        public Animal SellAnimal(int index)
        {
            if (index >= 0 && index < animals.Count)
            {
                Animal animal = animals[index];
                animals.RemoveAt(index);
                return animal;
            }
            return null;
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

        public void ShowStatus()
        {
            Console.WriteLine($"----- Статус тварин у зоомагазині \"{Name}\" -----");
            int i = 1;
            foreach (var animal in animals)
            {
                Console.WriteLine($"{i++}. Тварина: {animal.Name}, Жива: {animal.IsAlive}, Щаслива: {animal.IsHappy}, Прийомів сьогодні: {animal.MealCountToday}");
            }
        }
    }
}
