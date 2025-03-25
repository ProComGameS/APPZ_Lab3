using System;
using System.Collections.Generic;

namespace APPZ_Lab2
{

    // Console–інтерфейс симуляції. Всі операції вводу-виводу винесені сюди – логіка доменної області (Animal тощо) не містить прямого взаємодії з консоллю.

    class Program
    {
        static void Main(string[] args)
        {
            int currentTime = 0; // поточний час симуляції (х в годинах)
            // Для перевірки закінчення доби зберігаємо попередній день:
            int lastProcessedDay = currentTime / 24;

            // Створюємо хазяїна
            Owner owner = new Owner("Ivan");

            // Створюємо тварин за допомогою фабрики
            Animal dog = AnimalFactory.CreateAnimal("dog", "Rex", currentTime);
            Animal canary = AnimalFactory.CreateAnimal("canary", "Tweety", currentTime);
            Animal lizard = AnimalFactory.CreateAnimal("lizard", "Lizzy", currentTime);

            // Додаємо тварин до хазяїна
            owner.AddAnimal(dog);
            owner.AddAnimal(canary);
            owner.AddAnimal(lizard);

            // Підписка хазяїна на події всіх тварин
            dog.AnimalStateChanged += owner.AnimalStateChangedHandler;
            canary.AnimalStateChanged += owner.AnimalStateChangedHandler;
            lizard.AnimalStateChanged += owner.AnimalStateChangedHandler;

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n----- Стан симуляції -----");
                Console.WriteLine($"Поточний час (годин): {currentTime}");
                Console.WriteLine("1. Прогрес часу");
                Console.WriteLine("2. Годувати тварину");
                Console.WriteLine("3. Виконати дію (біг, хода, спів, політ) для тварини");
                Console.WriteLine("4. Прибрати тварину");
                Console.WriteLine("5. Випустити тварину на волю");
                Console.WriteLine("6. Показати статус тварин");
                Console.WriteLine("7. Вихід");
                Console.Write("Оберіть опцію: ");
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Введіть кількість годин для просування: ");
                        if (int.TryParse(Console.ReadLine(), out int hours))
                        {
                            int previousDay = currentTime / 24;
                            currentTime += hours;
                            int currentDay = currentTime / 24;
                            // Для кожної завершеної доби перевіряємо прийоми їжі
                            for (int day = previousDay + 1; day <= currentDay; day++)
                            {
                                Console.WriteLine($"Добу {day} завершено. Перевірка прийомів їжі для тварин...");
                                foreach (var animal in owner.Animals)
                                {
                                    animal.CheckDailyFeeding(currentTime);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Невірний формат кількості годин.");
                        }
                        break;

                    case "2":
                        {
                            Animal selectedAnimal = SelectAnimal(owner);
                            if (selectedAnimal != null)
                            {
                                selectedAnimal.Eat(currentTime);
                            }
                        }
                        break;

                    case "3":
                        {
                            Animal selectedAnimal = SelectAnimal(owner);
                            if (selectedAnimal != null)
                            {
                                Console.WriteLine("Оберіть дію: 1. Біг, 2. Хода, 3. Спів, 4. Політ");
                                string act = Console.ReadLine();
                                switch (act)
                                {
                                    case "1":
                                        selectedAnimal.Run(currentTime);
                                        break;
                                    case "2":
                                        selectedAnimal.Walk(currentTime);
                                        break;
                                    case "3":
                                        selectedAnimal.Sing(currentTime);
                                        break;
                                    case "4":
                                        selectedAnimal.Fly(currentTime);
                                        break;
                                    default:
                                        Console.WriteLine("Невірна дія.");
                                        break;
                                }
                            }
                        }
                        break;

                    case "4":
                        {
                            Animal selectedAnimal = SelectAnimal(owner);
                            if (selectedAnimal != null)
                            {
                                selectedAnimal.Clean(currentTime);
                            }
                        }
                        break;

                    case "5":
                        {
                            Animal selectedAnimal = SelectAnimal(owner);
                            if (selectedAnimal != null)
                            {
                                selectedAnimal.Release(currentTime);
                            }
                        }
                        break;

                    case "6":
                        {
                            Console.WriteLine("----- Статус тварин -----");
                            foreach (var animal in owner.Animals)
                            {
                                Console.WriteLine($"Тварина: {animal.Name}, Жива: {animal.IsAlive}, " +
                                    $"Випущена: {animal.IsReleased}, Щаслива: {animal.IsHappy}, " +
                                    $"Останній прийом їжі: {animal.LastMealTime}, Прийомів сьогодні: {animal.MealCountToday}");
                            }
                        }
                        break;

                    case "7":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Невірна опція.");
                        break;
                }
            }
        }

        // Допоміжний метод для вибору тварини із списку хазяїна.

        static Animal SelectAnimal(Owner owner)
        {
            Console.WriteLine("Оберіть тварину:");
            var animalList = new List<Animal>(owner.Animals);
            for (int i = 0; i < animalList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {animalList[i].Name}");
            }
            if (int.TryParse(Console.ReadLine(), out int selection))
            {
                if (selection >= 1 && selection <= animalList.Count)
                {
                    return animalList[selection - 1];
                }
            }
            Console.WriteLine("Невірний вибір.");
            return null;
        }
    }
}
