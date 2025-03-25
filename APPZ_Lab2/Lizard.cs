using System;

public class Lizard : Animal
{
    public Lizard(string name) : base(name)
    {
        HungryEvent += HandleHungryEvent; // Підписка на подію
    }

    public override void MakeSound()
    {
        if (!IsHungry)
        {
            Console.WriteLine($"{Name} is making a hissing sound.");
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
            Console.WriteLine($"{Name} is running.");
        }
        else
        {
            Console.WriteLine($"{Name} is crawling because it is too hungry to run.");
        }
    }

    public void PerformAction()
    {
        if (!IsHungry)
        {
            Move();
        }
        MakeSound();
    }

    private void HandleHungryEvent(object sender, EventArgs e)
    {
        Console.WriteLine($"The lizard {Name} is hungry and cannot run properly.");
    }
}
