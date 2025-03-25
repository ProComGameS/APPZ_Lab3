using System;

public class Canary : Animal, ISinger, IFlyer
{
    public Canary(string name) : base(name)
    {
        HungryEvent += HandleHungryEvent; // Підписка на подію
    }

    public override void MakeSound()
    {
        if (!IsHungry)
        {
            Console.WriteLine($"{Name} is singing a melody.");
        }
        else
        {
            Console.WriteLine($"{Name} is too hungry to make a sound.");
        }
    }

    public override void Move()
    {
        if (!IsHungry || DateTime.Now - LastFedTime <= TimeSpan.FromHours(8))
        {
            Console.WriteLine($"{Name} is flying.");
        }
        else
        {
            Console.WriteLine($"{Name} is walking because it is too hungry to fly.");
        }
    }

    public void Sing()
    {
        if (!IsHungry)
        {
            Console.WriteLine($"{Name} is singing happily.");
        }
        else
        {
            Console.WriteLine($"{Name} is too hungry to sing.");
        }
    }

    public void Fly()
    {
        if (!IsHungry)
        {
            Console.WriteLine($"{Name} is flying gracefully.");
        }
        else
        {
            Console.WriteLine($"{Name} is too hungry to fly.");
        }
    }

    public void PerformAction()
    {
        if (!IsHungry)
        {
            Sing();
            Fly();
        }
        MakeSound();
    }

    private void HandleHungryEvent(object sender, EventArgs e)
    {
        Console.WriteLine($"The canary {Name} is hungry and cannot sing or fly properly.");
    }
}
