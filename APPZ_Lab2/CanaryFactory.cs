namespace APPZ_Lab2
{
   
    public class CanaryFactory : IAnimalFactory
    {
        public Animal CreateAnimal(string name, int currentTime)
        {
            return new Canary(name, currentTime);
        }
    }
}
