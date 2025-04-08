namespace APPZ_Lab2
{

    public interface IAnimalFactory
    {
        Animal CreateAnimal(string name, int currentTime);
    }
}
