using System;
using System.Collections.Generic;

namespace APPZ_Lab2
{

    class Program
    {
        static void Main(string[] args)
        {
            
            int currentTime = 0; // Поточний час симуляції (в годинах)
            Console.OutputEncoding = System.Text.Encoding.UTF8;



            // Створюємо власника та зоомагазин.
            Owner owner = new Owner("John");
            PetShop petShop = new PetShop("Lapki");

            // Створення тварин
            IAnimalFactory dogFactory = new DogFactory();
            IAnimalFactory canaryFactory = new CanaryFactory();
            IAnimalFactory lizardFactory = new LizardFactory();

            Animal dog = CreateAnimal(dogFactory, "Rex", currentTime);
            Animal canary = CreateAnimal(canaryFactory, "Tweety", currentTime);
            Animal lizard = CreateAnimal(lizardFactory, "Lizzy", currentTime);

            petShop.AddAnimal(dog);
            petShop.AddAnimal(canary);
            petShop.AddAnimal(lizard);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n----- Стан симуляції -----");
                Console.WriteLine($"Поточний час (годин): {currentTime}");
                Console.WriteLine("1. Прогрес часу");
                Console.WriteLine("2. Забрати тварину з зоомагазину $");
                Console.WriteLine("3. Годувати тварину *");
                Console.WriteLine("4. Виконати дію для тварини *");
                Console.WriteLine("5. Чистити тварину *");
                Console.WriteLine("6. Випустити тварину на волю *");
                Console.WriteLine("7. Показати статус тварин власника");
                Console.WriteLine("8. Показати статус тварин зоомагазину");
                Console.WriteLine("9. Вихід");
                Console.Write("Оберіть опцію: ");

                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Введіть кількість годин для просування часу: ");
                        if (int.TryParse(Console.ReadLine(), out int hours))
                        {
                            currentTime += hours;
                            Console.WriteLine($"Час просунуто. Поточний час: {currentTime} годин.");

                            // Після завершення доби (кожні 24 години) перевіряємо годування
                            foreach (var animal in owner.Animals)
                                animal.CheckDailyFeeding(currentTime);
                            foreach (var animal in petShop.Animals)
                                animal.CheckDailyFeeding(currentTime);
                        }
                        else
                        {
                            Console.WriteLine("Невірний формат вводу.");
                        }
                        break;

                    case "2":
                        PickupAnimal(owner, petShop);
                        break;

                    case "3":
                        {
                            Animal a = SelectOwnerAnimal(owner, "обрати тварину для годування");
                            if (a != null)
                                a.Eat(currentTime);
                        }
                        break;

                    case "4":
                        {
                            Animal a = SelectOwnerAnimal(owner, "обрати тварину для виконання дії");
                            if (a != null)
                            {
                                Console.WriteLine("Оберіть дію:");
                                Console.WriteLine("1. Біг");
                                Console.WriteLine("2. Хода");
                                Console.WriteLine("3. Спів");
                                Console.WriteLine("4. Політ");
                                Console.Write("Ваш вибір: ");
                                string act = Console.ReadLine();
                                switch (act)
                                {
                                    case "1":
                                        a.Run(currentTime);
                                        break;
                                    case "2":
                                        a.Walk(currentTime);
                                        break;
                                    case "3":
                                        a.Sing(currentTime);
                                        break;
                                    case "4":
                                        a.Fly(currentTime);
                                        break;
                                    default:
                                        Console.WriteLine("Невірна дія.");
                                        break;
                                }
                            }
                        }
                        break;

                    case "5":
                        {
                            Animal a = SelectOwnerAnimal(owner, "обрати тварину для прибирання");
                            if (a != null)
                                a.Clean(currentTime);
                        }
                        break;

                    case "6":
                        {
                            Animal a = SelectOwnerAnimal(owner, "обрати тварину для випуску на волю");
                            if (a != null)
                                a.Release(currentTime);
                        }
                        break;

                    case "7":
                        {
                            Console.WriteLine("----- Статус тварин власника -----");
                            int i = 1;
                            foreach (var animal in owner.Animals)
                                Console.WriteLine($"{i++}. Тварина: {animal.Name}, Жива: {animal.IsAlive}, Випущена: {animal.IsReleased}, Щаслива: {animal.IsHappy}, Останній прийом їжі: {animal.LastMealTime}, Прийомів сьогодні: {animal.MealCountToday}");
                        }
                        break;

                    case "8":
                        petShop.ShowStatus();
                        break;

                    case "9":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Невірна опція.");
                        break;
                }
            }
            Console.WriteLine("Вихід із симуляції.");
        }


        static Animal CreateAnimal(IAnimalFactory factory, string name, int currentTime)
        {
            return factory.CreateAnimal(name, currentTime);
        }


        // Метод для вибору тварини з колекції власника.

        static Animal SelectOwnerAnimal(Owner owner, string prompt)
        {
            List<Animal> animalList = new List<Animal>(owner.Animals);
            if (animalList.Count == 0)
            {
                Console.WriteLine("У власника немає тварин. Спочатку заберіть тварину із зоомагазину.");
                return null;
            }
            Console.WriteLine($"Оберіть тварину для {prompt}:");
            for (int i = 0; i < animalList.Count; i++)
                Console.WriteLine($"{i + 1}. {animalList[i].Name}");
            if (int.TryParse(Console.ReadLine(), out int selection))
            {
                if (selection >= 1 && selection <= animalList.Count)
                    return animalList[selection - 1];
            }
            Console.WriteLine("Невірний вибір.");
            return null;
        }

        /// <summary>
        /// Метод для забрання (продажу) тварини із зоомагазину.
        /// Обрана тварина видаляється із зоомагазину і додається до власника.
        /// Також підписується обробник подій власника.
        /// </summary>
        static void PickupAnimal(Owner owner, PetShop petShop)
        {
            List<Animal> shopList = new List<Animal>(petShop.Animals);
            if (shopList.Count == 0)
            {
                Console.WriteLine("У зоомагазині немає тварин.");
                return;
            }
            Console.WriteLine("Оберіть тварину для забирання із зоомагазину:");
            for (int i = 0; i < shopList.Count; i++)
                Console.WriteLine($"{i + 1}. {shopList[i].Name}");
            if (int.TryParse(Console.ReadLine(), out int sel))
            {
                if (sel >= 1 && sel <= shopList.Count)
                {
                    Animal chosen = petShop.SellAnimal(sel - 1);
                    if (chosen != null)
                    {
                        // Позначаємо, що тварина більше не знаходиться в магазині:
                        chosen.IsInPetShop = false;
                        owner.AddAnimal(chosen);
                        // Підписка власника на події тварини
                        chosen.AnimalStateChanged += owner.AnimalStateChangedHandler;
                        Console.WriteLine($"Тварину {chosen.Name} забрано з зоомагазину і додано до вашого будинку.");
                    }
                    else
                        Console.WriteLine("Помилка при забиранні тварини.");
                }
                else
                    Console.WriteLine("Невірний вибір.");
            }
            else
                Console.WriteLine("Невірний формат вводу.");
        }

    }
}
