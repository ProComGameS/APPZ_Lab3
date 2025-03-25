using System;

namespace APPZ_Lab2
{
    /// <summary>
    /// Фабрика для створення об’єктів тварин за типом.
    /// Використання шаблону Factory ізолює логіку конструювання тварин від решти коду.
    /// </summary>
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
