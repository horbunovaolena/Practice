
//Коротко: інтерфейс — щоб зовнішній код не залежав від конкретного класу.
//Абстрактний клас — щоб не копіювати спільний код у кожному нащадку.

//Разом вони дають гнучкість + відсутність дублювання.
//1.послідовність (Sequence)-Ряд значень, де кожне наступне залежить від попередніх:
//Fibonacci: 0, 1, 1, 2, 3, 5, 8, 13...    (next = prev + curr)
//Степені: 1, 2, 4, 8, 16, 32...          (next = curr² / prev)
//Символи: A, B, B, C, D, F, I ...          (next = (prev + curr) % 26 + 'A')
//public        → всі
//protected set → тільки цей клас і нащадки
//private set   → тільки цей клас
//abstract      → немає тіла, нащадок ЗОБОВ'ЯЗАНИЙ реалізувати
//override      → нащадок ЗАМІНЮЄ абстрактний метод своєю логікою



//Коротко: ISequenceGenerator<T> — це "шаблон-контракт" для генераторів послідовностей
// будь-якого типу. I = інтерфейс, <T> = підставте будь-який тип.

// interface — контракт, який Каже ЩО клас повинен вміти, але не ЯК,бо
//            ЯК — визначатимуть класи, що його реалізують (наступні кроки)
//  < T > — узагальнення(generic), тобто працює з будь-яким типом (int, double, char тощо)
//  При використанні замінюється на конкретний тип.


using System.Security.Cryptography.X509Certificates;

public interface ISequenceGenerator<T> //Interface (конвенція: інтерфейси починаються з "I")
{
    T Previous { get; }
    T Current { get; }
    T Next { get; } 
}

// 1. Абстрактний клас РЕАЛІЗУЄ інтерфейс
//Abstract class (Абстрактний клас) — неповна реалізація
//Реалізує спільну логіку, а конкретне — залишає нащадкам:

public abstract class SequenceGenerator<T> : ISequenceGenerator<T>
{
    // ✅ Реалізовано — спільне для ВСІХ генераторів:
    protected T previous;
    protected T current;
    public T Previous => previous;
    public T Current => current;
    public T Next => GetNext();
    public int Count { get; protected set; }

    //protected — конструктор може викликатись тільки з класів-нащадків
    //  (бо клас абстрактний — не можна створити new SequenceGenerator() напряму)
    //  Приймає два параметри типу T — перші два значення послідовності
    protected SequenceGenerator(T first, T second)
    {
        previous = first;
        current = second;
        Count = 2;
    }

    // abstract — немає тіла { }, бо кожна послідовність рахує по-своєму
    // Кожен нащадок зобов'язаний реалізувати цей метод
    public abstract T GetNext(); 
}


    public class FibonacciSequenceGenerator : SequenceGenerator<int>
    {
    // Конструктор: приймає два перші числа і передає їх "батьку" через base
    public FibonacciSequenceGenerator(int first, int second)
         : base(first, second) { 
    
    
    public override int GetNext()
        {
            int next = previous + current;
            previous = current;
            current = next;
            Count++;
            return next;
        }
    
}
