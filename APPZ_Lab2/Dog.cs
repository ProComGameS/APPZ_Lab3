using System;

public class Dog : Animal, IRunner
{
    public Dog(string name) : base(name)
    {
        HungryEvent += HandleHungryEvent; // Підписка на подію
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} is barking.");
    }

    public override void Move()
    {
        if (!IsHungry || DateTime.Now - LastFedTime <= TimeSpan.FromHours(8))
        {
            Console.WriteLine($"{Name} is running.");
        }
        else
        {
            Console.WriteLine($"{Name} is walking.");
        }
    }

    public void Run()
    {
        if (!IsHungry)
        {
            Console.WriteLine($"{Name} is happily running.");
        }
        else
        {
            Console.WriteLine($"{Name} is too hungry to run.");
        }
    }

    public void PerformAction()
    {
        Run();
        MakeSound();
    }

    private void HandleHungryEvent(object sender, EventArgs e)
    {
        Console.WriteLine($"The dog {Name} is hungry and needs immediate attention!");
    }
}
