using System;
using System.Collections.Generic;

public class Owner
{
    public string Name { get; set; }
    public List<Animal> Pets { get; set; }

    public Owner(string name)
    {
        Name = name;
        Pets = new List<Animal>();
    }

    public void AddPet(Animal pet)
    {
        Pets.Add(pet);
        Console.WriteLine($"{Name} has adopted {pet.Name}.");
    }

    public void ReleasePet(Animal pet)
    {
        if (Pets.Contains(pet))
        {
            Pets.Remove(pet);
            Console.WriteLine($"{Name} has released {pet.Name} into the wild.");
        }
        else
        {
            Console.WriteLine($"{pet.Name} is not a pet of {Name}.");
        }
    }

    public void FeedAllPets()
    {
        foreach (var pet in Pets)
        {
            pet.Eat();
        }
    }

    public void CleanAllPets()
    {
        foreach (var pet in Pets)
        {
            pet.Clean();
        }
    }

    public void ShowPetStatus()
    {
        foreach (var pet in Pets)
        {
            Console.WriteLine($"{pet.Name} - Hungry: {pet.IsHungry}, Happy: {pet.IsHappy}");
        }
    }
}
