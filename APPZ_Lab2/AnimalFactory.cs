using System;

namespace APPZ_Lab2
{

    // Клас-інструмент (Factory Pattern) для створення тварин.
    // Завдяки цьому класу логіка створення ізольована від решти коду,
    // що дозволяє легко додавати нові типи тварин.

    public static class AnimalFactory
    {
        public static Animal CreateAnimal(string type, string name, int currentTime)
        {
            switch (type.ToLower())
            {
                case "dog":
                    return new Dog(name, currentTime);
                case "canary":
                    return new Canary(name, currentTime);
                case "lizard":
                    return new Lizard(name, currentTime);
                default:
                    throw new ArgumentException($"Невідомий тип тварини: {type}");
            }
        }
    }
}
