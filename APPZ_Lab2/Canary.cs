using System;

namespace APPZ_Lab2
{

    // Клас для канарки. Наслідує загальні властивості з Animal.
    // Перевизначено метод Run – канарка рухається по-іншому на землі.

    public class Canary : Animal
    {
        public Canary(string name, int currentTime) : base(name, currentTime) { }

        public override void Run(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                OnAnimalStateChanged($"{Name} занадто голодний(на), щоб бігати.", simulationTime);
                return;
            }
            OnAnimalStateChanged($"{Name} ледь пробігає по землі у годину {simulationTime}.", simulationTime);
        }
    }
}
