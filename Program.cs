using System;
using System.Globalization;

namespace DMB_Timer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU", false);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("DMB Timer v.3.1\n");

            switch (args.Length)
            {
                case 0:
                    UserInterface();
                    break;

                case 2:
                    ArgsParser(args);
                    break;

                default:
                    Console.WriteLine("Ошибка! Неверное количество аргументов.\nВведите две даты в формате ДД.ММ.ГГГГ");
                    break;
            }

            Console.WriteLine("\n(C) Piotr Kniaz, 2022. For exit press any key.");
            Console.ReadKey(true);
            return;
        }

        private static void ArgsParser(string[] args)
        {
            DateTime[] dates = new DateTime[2];
            bool error = false;

            for (int i = 0; i < 2; i++)
            {
                if (!DateTime.TryParse(args[i], out dates[i]))
                    error = true;
            }

            if (error)
            {
                Console.WriteLine("Ошибка! Неверный формат даты.");
                return;
            }

            if (DateTime.Compare(dates[0], dates[1]) >= 0)
            {
                Console.WriteLine("Ошибка! Дата ДМБ не может быть раньше, чем дата призыва.");
                return;
            }

            Calculate(dates[0], dates[1]);
            return;
        }

        private static void UserInterface()
        {
            Console.WriteLine("Формат даты: ДД.ММ.ГГГГ\nНапример, 22.02.2022");

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