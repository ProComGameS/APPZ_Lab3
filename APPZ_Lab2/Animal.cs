using System;

public abstract class Animal
{
    public string Name { get; set; }
    public bool IsHungry { get; set; }
    public bool IsHappy { get; set; }
    public DateTime LastFedTime { get; set; }
    public int FeedCount { get; set; }

    public event EventHandler HungryEvent;

    public Animal(string name)
    {
        Name = name;
        IsHungry = true;
        IsHappy = false;
        LastFedTime = DateTime.Now;
        FeedCount = 0;
    }

    public virtual void Eat()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan timeSinceLastFed = currentTime - LastFedTime;

        if (timeSinceLastFed.TotalHours >= 24)
        {
            Console.WriteLine($"{Name} has died of starvation.");
        }
        else if (FeedCount < 5 && timeSinceLastFed.TotalSeconds >= 20)
        {
            LastFedTime = currentTime;
            FeedCount++;
            IsHungry = false;
            Console.WriteLine($"{Name} is now fed.");
        }
        else
        {
            Console.WriteLine($"{Name} cannot eat more than 5 times a day or less than 4 hours apart.");
        }

        if (timeSinceLastFed.TotalSeconds > 20)
        {
            OnHungryEvent(EventArgs.Empty);
        }
    }

    protected virtual void OnHungryEvent(EventArgs e)
    {
        Console.WriteLine($"{Name} is hungry now!");
        HungryEvent?.Invoke(this, e);
    }

    public void Clean()
    {
        IsHappy = true;
        Console.WriteLine($"{Name} is now happy after cleaning.");
    }

    public abstract void MakeSound();

    public abstract void Move();
}
