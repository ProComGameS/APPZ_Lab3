using System;

namespace APPZ_Lab2
{
    /// <summary>
    /// Абстрактний клас, що визначає загальний функціонал та властивості тварин.
    /// Фізичні характеристики: очі, лапи, крила. Тварина повинна їсти від 1 до 5 разів на добу,
    /// і якщо з останнього годування пройшло більше 8 годин – деякі активності заборонені.
    /// Повідомлення про кожну дію супроводжуються ASCII‑артом, який перевизначається у дочірніх класах.
    /// Реалізовано шаблон Observer (події) для сповіщення про зміну стану тварини.
    /// </summary>
    public abstract class Animal
    {
        // Фізичні характеристики
        public int Eyes { get; protected set; }
        public int Legs { get; protected set; }
        public int Wings { get; protected set; }

        public string Name { get; protected set; }
        public bool IsAlive { get; protected set; }
        public bool IsReleased { get; protected set; }
        public bool IsHappy { get; protected set; }

        // Час останнього годування, лічильник прийомів їжі за добу, час останньої чистки.
        public int LastMealTime { get; protected set; }
        public int MealCountToday { get; protected set; }
        public int LastCleanTime { get; protected set; }

        // Нова властивість – чи знаходиться тварина в зоомагазині. За замовчуванням: true.
        public bool IsInPetShop { get; set; }

        public event EventHandler<AnimalEventArgs> AnimalStateChanged;

        public Animal(string name, int currentTime)
        {
            Name = name;
            IsAlive = true;
            IsReleased = false;
            IsHappy = false;
            LastMealTime = currentTime;
            MealCountToday = 0;
            LastCleanTime = currentTime;

            // За замовчуванням: 2 очі, 4 лапи, 2 крила.
            Eyes = 2;
            Legs = 4;
            Wings = 2;

            // При створенні тварина знаходиться в зоомагазині.
            IsInPetShop = true;
        }

        /// <summary>
        /// Метод, що повертає ASCII‑мистецтво для дій.
        /// У базовому класі повертається порожній рядок; перевизначається в дочірніх класах.
        /// </summary>
        protected virtual string GetAsciiArt()
        {
            return "";
        }

        /// <summary>
        /// Допоміжний метод для сповіщення: додає до повідомлення ASCII‑арт.
        /// </summary>
        protected void ReportAction(string message, int simulationTime)
        {
            OnAnimalStateChanged(message + "\n" + GetAsciiArt(), simulationTime);
        }

        protected virtual void OnAnimalStateChanged(string message, int simulationTime)
        {
            AnimalStateChanged?.Invoke(this, new AnimalEventArgs(message, this, simulationTime));
        }

        /// <summary>
        /// Перевірка: чи минуло не більше 8 годин від останнього годування.
        /// </summary>
        protected bool CheckFoodForActivity(int simulationTime)
        {
            return (simulationTime - LastMealTime) <= 8;
        }

        public virtual void Eat(int simulationTime)
        {
            if (!IsAlive) return;
            MealCountToday++;
            LastMealTime = simulationTime;
            ReportAction($"{Name} був(ла) нагодований(а) у годину {simulationTime}. Прийомів сьогодні: {MealCountToday}.", simulationTime);
        }

        public virtual void Clean(int simulationTime)
        {
            if (!IsAlive) return;
            LastCleanTime = simulationTime;
            UpdateHappiness(simulationTime);
            ReportAction($"{Name} було прибрано у годину {simulationTime}. Тепер тварина {(IsHappy ? "щаслива" : "не щаслива")}.", simulationTime);
        }

        public virtual void Release(int simulationTime)
        {
            if (!IsAlive) return;
            IsReleased = true;
            UpdateHappiness(simulationTime);
            ReportAction($"{Name} випущено на волю у годину {simulationTime}.", simulationTime);
        }

        public virtual void Run(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                ReportAction($"{Name} занадто голодний(на), щоб бігати (понад 8 годин без їжі).", simulationTime);
                return;
            }
            ReportAction($"{Name} біжить у годину {simulationTime}.", simulationTime);
        }

        public virtual void Walk(int simulationTime)
        {
            if (!IsAlive) return;
            ReportAction($"{Name} йде у годину {simulationTime}.", simulationTime);
        }

        public virtual void Sing(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                ReportAction($"{Name} занадто голодний(на), щоб співати (понад 8 годин без їжі).", simulationTime);
                return;
            }
            ReportAction($"{Name} співає у годину {simulationTime}.", simulationTime);
        }

        public virtual void Fly(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                ReportAction($"{Name} занадто голодний(на), щоб літати (понад 8 годин без їжі).", simulationTime);
                return;
            }
            ReportAction($"{Name} летить у годину {simulationTime}.", simulationTime);
        }

        /// <summary>
        /// Перевірка щоденного годування: якщо прийомів < 1 або > 5 – тварина помирає.
        /// Але якщо тварина знаходиться в зоомагазині, перевірка не проводиться.
        /// </summary>
        public virtual void CheckDailyFeeding(int simulationTime)
        {
            if (IsInPetShop)
            {
                OnAnimalStateChanged($"{Name} знаходиться у зоомагазині, де йому завжди надають належний догляд.", simulationTime);
                MealCountToday = 0; // скидання лічильника
                return;
            }

            if (MealCountToday < 1 || MealCountToday > 5)
            {
                IsAlive = false;
                OnAnimalStateChanged($"{Name} помер(ла) через невідповідну кількість годувань: {MealCountToday} прийом(ів) за добу.", simulationTime);
            }
            else
            {
                OnAnimalStateChanged($"{Name} вижив(ла) день з {MealCountToday} прийомами їжі.", simulationTime);
            }
            MealCountToday = 0;
        }

        protected virtual void UpdateHappiness(int simulationTime)
        {
            IsHappy = (simulationTime - LastCleanTime <= 24) || IsReleased;
        }
    }
}
