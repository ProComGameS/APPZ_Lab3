using System;

namespace APPZ_Lab2
{

    // Клас для ящірки. Наслідує загальні властивості з Animal.
    // Для ящірки методи Sing та Fly не реалізовано – вона не має таких здібностей.
    // Метод Run трактовано як повзання із швидким переміщенням.

    public class Lizard : Animal
    {
        public Lizard(string name, int currentTime) : base(name, currentTime) { }

        public override void Run(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                OnAnimalStateChanged($"{Name} занадто голодний(на), щоб швидко повзати.", simulationTime);
                return;
            }
            OnAnimalStateChanged($"{Name} швидко повзе у годину {simulationTime}.", simulationTime);
        }

        public override void Sing(int simulationTime)
        {
            if (!IsAlive) return;
            OnAnimalStateChanged($"{Name} не може співати.", simulationTime);
        }

        public override void Fly(int simulationTime)
        {
            if (!IsAlive) return;
            OnAnimalStateChanged($"{Name} не може літати.", simulationTime);
        }
    }
}
