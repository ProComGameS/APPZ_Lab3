using System;

namespace APPZ_Lab2
{
    /// <summary>
    /// Клас для ящірки.
    /// Дії (повзання, годування тощо) супроводжуються ASCII‑артом.
    /// </summary>
    public class Lizard : Animal
    {
        private const string LizardAscii = @"⢀⣤⠴⠖⠋⠉⠓⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⣿⣄⠀⠂⠀⢶⣿⣇⡙⠷⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠈⢳⡝⠢⡀⠀⠁⠀⠙⠦⣈⢻⡄⠀⠀⠀⠀⣠⢖⣶⡶⠶⠚⠛⠉⣉⠭⠝⠛⠋⠉⠉⠉⠛⠛⠓⠒⠶⠤⣤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠙⣦⠈⠲⠄⣀⠀⢾⡏⠑⠿⡦⣤⣴⠞⠛⢉⣁⣀⠠⠤⠒⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠓⠶⣤⡀⠀⠀⠀⠀
⠀⠀⠀⠈⠳⣄⡀⠀⠙⢓⡆⠠⢲⢾⣖⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡔⠀⢦⠤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠳⣄⠀⠀
⠀⠀⠀⠀⠀⠀⠉⢳⣦⣿⣷⣾⣿⡿⢏⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡇⠀⠀⣗⠛⠋⠉⠉⠉⠙⠛⠒⠶⢤⣄⡀⠀⠀⠈⢳⡄";

        public Lizard(string name, int currentTime) : base(name, currentTime)
        {
            Eyes = 2;
            Legs = 4;
            Wings = 2;
        }

        protected override string GetAsciiArt()
        {
            return LizardAscii;
        }

        public override void Run(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                ReportAction($"{Name} занадто голодний(на), щоб швидко повзати.", simulationTime);
                return;
            }
            ReportAction($"{Name} повзе у годину {simulationTime}.", simulationTime);
        }

        public override void Sing(int simulationTime)
        {
            if (!IsAlive) return;
            ReportAction($"{Name} не може співати.", simulationTime);
        }

        public override void Fly(int simulationTime)
        {
            if (!IsAlive) return;
            ReportAction($"{Name} не може літати.", simulationTime);
        }
    }
}
