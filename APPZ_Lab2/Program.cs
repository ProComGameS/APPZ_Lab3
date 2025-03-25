using System;

public class Program
{
    public static void Main()
    {
        
        Owner owner = new Owner("John");
        PetStore petStore = new PetStore();

       
        Dog dog = new Dog("Rex");
        Canary canary = new Canary("Tweety");
        Lizard lizard = new Lizard("Lizzy");

       
        petStore.AddAnimal(dog);
        petStore.AddAnimal(canary);
        petStore.AddAnimal(lizard);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Show Pet Store Animals");
            Console.WriteLine("2. Show Owner's Pets");
            Console.WriteLine("3. Buy Animal from Pet Store");
            Console.WriteLine("4. Feed Owner's Pets");
            Console.WriteLine("5. Clean Owner's Pets");
            Console.WriteLine("6. Release Owner's Pet");
            Console.WriteLine("7. Perform Action with Animal");
            Console.WriteLine("8. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                   
                    petStore.ShowAnimalStatus();
                    break;

                case "2":
                    
                    owner.ShowPetStatus();
                    break;

                case "3":
                   
                    Console.Write("Enter animal name to buy: ");
                    string animalName = Console.ReadLine();
                    Animal animalToBuy = petStore.Animals.Find(a => a.Name == animalName);
                    if (animalToBuy != null)
                    {
                        petStore.SellAnimal(animalToBuy, owner);
                    }
                    else
                    {
                        Console.WriteLine("Animal not found in pet store.");
                    }
                    break;

                case "4":
                    
                    owner.FeedAllPets();
                    break;

                case "5":
                   
                    owner.CleanAllPets();
                    break;

                case "6":
                   
                    Console.Write("Enter animal name to release: ");
                    string animalNameToRelease = Console.ReadLine();
                    Animal animalToRelease = owner.Pets.Find(a => a.Name == animalNameToRelease);
                    if (animalToRelease != null)
                    {
                        owner.ReleasePet(animalToRelease);
                    }
                    else
                    {
                        Console.WriteLine("Animal not found among owner's pets.");
                    }
                    break;

                case "7":
                  
                    Console.Write("Enter animal name to perform an action: ");
                    string actionAnimalName = Console.ReadLine();
                    Animal actionAnimal = owner.Pets.Find(a => a.Name == actionAnimalName);
                    if (actionAnimal != null)
                    {
                        if (actionAnimal is Dog d)
                        {
                            d.PerformAction();
                        }
                        else if (actionAnimal is Canary c)
                        {
                            c.PerformAction();
                        }
                        else if (actionAnimal is Lizard l)
                        {
                            l.PerformAction();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Animal not found among owner's pets.");
                    }
                    break;

                case "8":
                   
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
