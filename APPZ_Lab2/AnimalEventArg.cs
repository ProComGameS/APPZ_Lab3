using System;

namespace APPZ_Lab2
{
    
    // Аргументи для подій зміни стану тварини.
    
    public class AnimalEventArgs : EventArgs
    {
        public string Message { get; }
        public Animal Animal { get; }
        public int SimulationTime { get; }

        public AnimalEventArgs(string message, Animal animal, int simulationTime)
        {
            Message = message;
            Animal = animal;
            SimulationTime = simulationTime;
        }
    }
}
