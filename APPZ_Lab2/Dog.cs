using System;

namespace APPZ_Lab2
{

    // Клас для собаки. Наслідує загальні властивості з Animal.
    // Перевизначено метод Sing – замість співу собака гавкає.
    // Методу Fly надано конкретну реалізацію – собака не може літати.

    public class Dog : Animal
    {
        public Dog(string name, int currentTime) : base(name, currentTime) { }

        public override void Sing(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                OnAnimalStateChanged($"{Name} занадто голодний(на), щоб гавкати.", simulationTime);
                return;
            }
            OnAnimalStateChanged($"{Name} гавкaє у годину {simulationTime}.", simulationTime);
        }

        public override void Fly(int simulationTime)
        {
            if (!IsAlive) return;
            OnAnimalStateChanged($"{Name} не може літати.", simulationTime);
        }
    }
}
