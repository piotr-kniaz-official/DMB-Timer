using System;

namespace DMB_Timer
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var today = DateTime.Today;
            string buf;

            Console.WriteLine("DMB Timer v.1.3\n");
            Console.WriteLine("Формат даты: DD.MM.YYYY\nНапример, 22.02.2022\n");

            Console.Write("Введите дату призыва: ");
            buf = Console.ReadLine();
            if (!DateTime.TryParse(buf, out DateTime startDate))
            {
                Console.WriteLine("Ошибка! Неверный формат даты.\nНажмите любую клавишу для выхода.");
                Console.ReadKey(true);
                return;
            }
            if (DateTime.Compare(startDate, today) > 0)
            {
                Console.WriteLine("\n\tСлужба еще не началась!");
                Console.WriteLine("\n(C) Piotr Kniaz, 2022\nНажмите любую клавишу для выхода.");
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

            var total = finishDate.Subtract(startDate);
            var left = finishDate.Subtract(today);

            int leftDays = (left.TotalDays < 0) ? 0 : Convert.ToInt32(left.TotalDays);
            double percent = 100 / (total.TotalDays + 1) * (total.TotalDays + 1 - left.TotalDays);
            percent = (percent > 100) ? 100 : Math.Round(percent, 2);

            Console.WriteLine();
            Console.WriteLine($"Сегодня:       {today.ToShortDateString()}");
            Console.WriteLine();
            Console.WriteLine($"\tВсего дней:    {total.TotalDays + 1}"); // день ДМБ так же считаем
            Console.WriteLine($"\tОсталось дней: {leftDays}");
            Console.WriteLine($"\tПройдено:      {percent} %");

            if (left.TotalDays == 0)
                Console.WriteLine("\n\tДМБ сегодня!");

            if (left.TotalDays < 0)
                Console.WriteLine($"\n\tСлужба завершена {-left.TotalDays} дней назад!");

            Console.WriteLine("\n(C) Piotr Kniaz, 2022\nНажмите любую клавишу для выхода.");
            Console.ReadKey(true);
            return;
        }
    }
}
