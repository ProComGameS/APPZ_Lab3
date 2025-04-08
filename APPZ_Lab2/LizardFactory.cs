namespace APPZ_Lab2
{
 
    public class LizardFactory : IAnimalFactory
    {
        public Animal CreateAnimal(string name, int currentTime)
        {
            return new Lizard(name, currentTime);
        }
    }
}
