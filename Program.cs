using System;

namespace DMB_Timer
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("DMB Timer v.2.0\n");
            Console.WriteLine("Формат даты: DD.MM.YYYY\nНапример, 22.02.2022");

            while (true)
            {
                string buf;

                Console.Write("\nВведите дату призыва:\t");
                buf = Console.ReadLine();
                if (!DateTime.TryParse(buf, out DateTime startDate))
                {
                    Console.WriteLine("Ошибка! Неверный формат даты.\nНажмите любую клавишу чтобы попробовать еще раз.");
                    Console.ReadKey(true);
                    continue;
                }

                Console.Write("Введите дату ДМБ:\t");
                buf = Console.ReadLine();
                if (!DateTime.TryParse(buf, out DateTime finishDate))
                {
                    Console.WriteLine("Ошибка! Неверный формат даты.\nНажмите любую клавишу чтобы попробовать еще раз.");
                    Console.ReadKey(true);
                    continue;
                }

                if (DateTime.Compare(startDate, finishDate) >= 0)
                {
                    Console.WriteLine("Ошибка! Дата ДМБ не может быть раньше, чем дата призыва.\nНажмите любую клавишу чтобы попробовать еще раз.");
                    Console.ReadKey(true);
                    continue;
                }

                Calculate(startDate, finishDate);

                while (true)
                {
                    Console.Write("\nВыйти из программы? (Y/N): ");
                    buf = Console.ReadLine();

                    if (buf == "N" || buf == "n" || buf == "Y" || buf == "y")
                        break;

                    Console.WriteLine("Ошибка! Введите 'Y' или 'N'.");
                }

                if (buf == "N" || buf == "n")
                    continue;

                break;
            }

            Console.WriteLine("\n(C) Piotr Kniaz, 2022. For exit press any key.");
            Console.ReadKey(true);
            return;
        }

        private static void Calculate(DateTime startDate, DateTime finishDate)
        {
            DateTime today = DateTime.Today;

            if (DateTime.Compare(startDate, today) > 0)
            {
                var leftBeforeStart = startDate.Subtract(today);
                Console.WriteLine($"\n\tСлужба начнется через {leftBeforeStart.TotalDays} дней.");
                return;
            }

            var total = finishDate.Subtract(startDate);
            var leftBeforeFinish = finishDate.Subtract(today);

            int leftDays = (leftBeforeFinish.TotalDays < 0) ? 0 : Convert.ToInt32(leftBeforeFinish.TotalDays);
            double percent = 100 / (total.TotalDays + 1) * (total.TotalDays + 1 - leftBeforeFinish.TotalDays);
            percent = (percent > 100) ? 100 : Math.Round(percent, 2);

            Console.WriteLine($"Сегодня:\t\t{today.ToShortDateString()}");
            Console.WriteLine($"\n\tВсего дней:\t{total.TotalDays + 1}"); // день ДМБ также считается
            Console.WriteLine($"\tОсталось дней:\t{leftDays}");
            Console.WriteLine($"\tПройдено:\t{percent} %");

            if (leftBeforeFinish.TotalDays == 0)
                Console.WriteLine("\n\tДМБ сегодня!");

            if (leftBeforeFinish.TotalDays < 0)
                Console.WriteLine($"\n\tСлужба завершена {-leftBeforeFinish.TotalDays} дней назад!");

            return;
        }
    }
}