namespace APPZ_Lab2
{

    public class DogFactory : IAnimalFactory
    {
        public Animal CreateAnimal(string name, int currentTime)
        {
            return new Dog(name, currentTime);
        }
    }
}
