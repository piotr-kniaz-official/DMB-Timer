using System;

namespace DMB_Timer
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string buf;

            Console.WriteLine("DMB Timer v.1.1\n");
            Console.WriteLine("Формат даты: DD.MM.YYYY\nНапример, 22.02.2022\n");

            Console.Write("Введите дату призыва: ");
            buf = Console.ReadLine();
            if (!DateTime.TryParse(buf, out DateTime startDate))
            {
                Console.WriteLine("Ошибка! Неверный формат даты.\nНажмите любую клавишу для выхода.");
                Console.ReadKey(true);
                return;
            }

            Console.Write("Введмте дату ДМБ:     ");
            buf = Console.ReadLine();
            if (!DateTime.TryParse(buf, out DateTime finishDate))
            {
                Console.WriteLine("Ошибка! Неверный формат даты.\nНажмите любую клавишу для выхода.");
                Console.ReadKey(true);
                return;
            }

            if (DateTime.Compare(startDate, finishDate) >= 0)
            {
                Console.WriteLine("Ошибка! Дата ДМБ не может быть раньше, чем дата призыва.\nНажмите любую клавишу для выхода.");
                Console.ReadKey(true);
                return;
            }

            var today = DateTime.Today;
            var total = finishDate.Subtract(startDate);
            var left = finishDate.Subtract(today);

            Console.WriteLine();
            Console.WriteLine($"Сегодня:       {today.ToShortDateString()}");
            Console.WriteLine();
            Console.WriteLine($"Всего дней:    {total.TotalDays + 1}"); // день ДМБ так же считаем
            Console.WriteLine($"Осталось дней: {left.TotalDays}");
            Console.WriteLine($"Пройдено:      {Math.Round(100 / (total.TotalDays + 1) * (total.TotalDays + 1 - left.TotalDays), 3)} %");

            Console.WriteLine("\n(C) Piotr Kniaz, 2022\nНажмите любую клавишу для выхода.");
            Console.ReadKey(true);
            return;
        }
    }
}
