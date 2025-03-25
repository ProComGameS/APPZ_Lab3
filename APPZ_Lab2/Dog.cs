using System;

namespace APPZ_Lab2
{
    /// <summary>
    /// Клас для собаки.
    /// Собака має 2 очі, 4 лапи (Wings = 0).
    /// При виконанні дій (годування, біг, хода, «спів» як гавкіт, прибирання, випуск) до повідомлення додається ASCII‑арт.
    /// </summary>
    public class Dog : Animal
    {
        private const string DogAscii = @"⠀⠀⠀⠀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢠⣤⡀⣾⣿⣿⠀⣤⣤⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⢿⣿⡇⠘⠛⠁⢸⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠈⣉⣤⣾⣿⣿⡆⠉⣴⣶⣶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣾⣿⣿⣿⣿⣿⣿⡀⠻⠟⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠙⠛⠻⢿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠈⠙⠋⠁⠀⠀⠀⠀⠀";

        public Dog(string name, int currentTime) : base(name, currentTime)
        {
            Eyes = 2;
            Legs = 4;
            Wings = 0;
        }

        protected override string GetAsciiArt()
        {
            return DogAscii;
        }

        
        public override void Sing(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                ReportAction($"{Name} занадто голодний(на), щоб гавкати.", simulationTime);
                return;
            }
            ReportAction($"{Name} гавкає у годину {simulationTime}.", simulationTime);
        }

        public override void Fly(int simulationTime)
        {
            if (!IsAlive) return;
            ReportAction($"{Name} не може літати.", simulationTime);
        }
    }
}
