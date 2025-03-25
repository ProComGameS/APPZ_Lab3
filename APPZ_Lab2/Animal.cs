using APPZ_Lab2;
using System;

namespace APPZ_Lab2
{

    // Абстрактний базовий клас для всіх тварин.
    // Містить загальну логіку: годування, прибирання, рух, перевірку стану (нагріта/щаслива/померла) тощо.
    // Використовується шаблон Observer – зміни стану повідомляються через подію AnimalStateChanged.

    public abstract class Animal
    {
        public string Name { get; protected set; }
        public bool IsAlive { get; protected set; }
        public bool IsReleased { get; protected set; }
        public bool IsHappy { get; protected set; }

        // Зберігаємо дані симуляції – години останнього прийому їжі, кількість прийомів їжі за день та останнє прибирання.
        public int LastMealTime { get; protected set; } // годинниця симуляції
        public int MealCountToday { get; protected set; }
        public int LastCleanTime { get; protected set; } // годинниця симуляції

        // Подія для повідомлення про зміну стану (Observer Pattern).
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
        }

      
        // Виклик події зміни стану.
    
        protected virtual void OnAnimalStateChanged(string message, int simulationTime)
        {
            AnimalStateChanged?.Invoke(this, new AnimalEventArgs(message, this, simulationTime));
        }

       
        // Метод годування.
        // Збільшує лічильник прийомів їжі та оновлює час останнього годування.
       
        public virtual void Eat(int simulationTime)
        {
            if (!IsAlive) return;
            MealCountToday++;
            LastMealTime = simulationTime;
            OnAnimalStateChanged($"{Name} був(ла) нагодований(а) у годину {simulationTime}. Кількість їжі за сьогодні: {MealCountToday}.", simulationTime);
        }

        // Метод прибирання. Визначається стан щастя: якщо тварину прибирають 1 раз на день або вона на волі – вона щаслива.
    
        public virtual void Clean(int simulationTime)
        {
            if (!IsAlive) return;
            LastCleanTime = simulationTime;
            // Якщо прибирання відбулося сьогодні (або в тварини нема догляду через волю), вона щаслива.
            if ((simulationTime - LastCleanTime) <= 24 || IsReleased)
                IsHappy = true;
            else
                IsHappy = false;
            OnAnimalStateChanged($"{Name} було прибрано у годину {simulationTime}. Тварина {(IsHappy ? "щаслива" : "не щаслива")}.", simulationTime);
        }

        
        // Метод для випуску тварини на волю.
        // Після випуску догляд припиняється, але тварина вважається щасливою за умовою.
        
        public virtual void Release(int simulationTime)
        {
            if (!IsAlive) return;
            IsReleased = true;
            IsHappy = true;
            OnAnimalStateChanged($"{Name} випущено на волю у годину {simulationTime}.", simulationTime);
        }

   
        // Перевірка, чи може тварина виконувати певні активності. Якщо з моменту останнього прийому їжі пройшло більш ніж 8 годин –
        // її фізична активність (біг, спів, політ) обмежується.
  
        protected bool CheckFoodForActivity(int simulationTime)
        {
            return (simulationTime - LastMealTime) <= 8;
        }

        // Методи для виконання дій. Якщо від останнього прийому їжі пройшло більше 8 годин – активність відхиляється.
        public virtual void Run(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                OnAnimalStateChanged($"{Name} занадто голодний(на), щоб бігти (останні 8 годин без їжі).", simulationTime);
                return;
            }
            OnAnimalStateChanged($"{Name} біжить у годину {simulationTime}.", simulationTime);
        }

        public virtual void Walk(int simulationTime)
        {
            if (!IsAlive) return;
            OnAnimalStateChanged($"{Name} йде у годину {simulationTime}.", simulationTime);
        }

        public virtual void Sing(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                OnAnimalStateChanged($"{Name} занадто голодний(на), щоб співати (останні 8 годин без їжі).", simulationTime);
                return;
            }
            OnAnimalStateChanged($"{Name} співає у годину {simulationTime}.", simulationTime);
        }

        public virtual void Fly(int simulationTime)
        {
            if (!IsAlive) return;
            if (!CheckFoodForActivity(simulationTime))
            {
                OnAnimalStateChanged($"{Name} занадто голодний(на), щоб літати (останні 8 годин без їжі).", simulationTime);
                return;
            }
            OnAnimalStateChanged($"{Name} летить у годину {simulationTime}.", simulationTime);
        }

        
        // Метод перевірки прийому їжі за день.
        // Якщо прийомів їжі менше 1 або більше 5 – тварина помирає.
        
        public virtual void CheckDailyFeeding(int simulationTime)
        {
            if (MealCountToday < 1 || MealCountToday > 5)
            {
                IsAlive = false;
                OnAnimalStateChanged($"{Name} помер(ла) через невідповідну частоту годувань (сьогодні прийом їжі: {MealCountToday} разів).", simulationTime);
            }
            else
            {
                OnAnimalStateChanged($"{Name} вижив(ла) день з {MealCountToday} прийомами їжі.", simulationTime);
            }
            // Скидання лічильника для наступного дня.
            MealCountToday = 0;
        }
    }
}
