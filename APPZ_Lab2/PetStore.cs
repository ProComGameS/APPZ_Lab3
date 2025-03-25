using System;
using System.Collections.Generic;

public class PetStore
{
    public List<Animal> Animals { get; set; }

    public PetStore()
    {
        Animals = new List<Animal>();
    }

    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
        Console.WriteLine($"{animal.Name} has been added to the pet store.");
    }

    public void SellAnimal(Animal animal, Owner newOwner)
    {
        if (Animals.Contains(animal))
        {
            Animals.Remove(animal);
            newOwner.AddPet(animal);
            Console.WriteLine($"{animal.Name} has been sold to {newOwner.Name}.");
        }
        else
        {
            Console.WriteLine($"{animal.Name} is not available in the pet store.");
        }
    }

    public void ShowAnimalStatus()
    {
        foreach (var animal in Animals)
        {
            Console.WriteLine($"{animal.Name} - Hungry: {animal.IsHungry}, Happy: {animal.IsHappy}");
        }
    }
}
