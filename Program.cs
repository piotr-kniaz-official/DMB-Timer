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

            Console.WriteLine("DMB Timer v.4.0\n");

            switch (args.Length)
            {
                case 0:
                    UserInterface();
                    break;

                case 2:
                    try
                    {
                        ArgsParser(args);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    Console.WriteLine("Ошибка! Неверное количество аргументов.\nВведите две даты в формате ДД.ММ.ГГГГ");
                    break;
            }

            Console.WriteLine("\n(C) Piotr Kniaz, 2022. For exit press any key.");
            Console.ReadKey(true);
        }

        private static void ArgsParser(string[] args)
        {
            DateTime[] dates = new DateTime[2];

            for (int i = 0; i < 2; i++)
                dates[i] = GetDate(args[i]);

            if (DateTime.Compare(dates[0], dates[1]) >= 0)
                throw new Exception("Ошибка! Дата ДМБ не может быть раньше, чем дата призыва.");

            Calculate(dates[0], dates[1]);
        }

        private static void UserInterface()
        {
            Console.WriteLine("Формат даты: ДД.ММ.ГГГГ\nНапример, 22.02.2022");

            while (true)
            {
                DateTime startDate, finishDate;
                try
                {
                    Console.Write("\nВведите дату призыва:\t");
                    startDate = GetDate();

                    Console.Write("Введите дату ДМБ:\t");
                    finishDate = GetDate();

                    if (DateTime.Compare(startDate, finishDate) >= 0)
                        throw new Exception("Ошибка! Дата ДМБ не может быть раньше, чем дата призыва.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                Calculate(startDate, finishDate);

                string quit;
                while (true)
                {
                    Console.Write("\nВыйти из программы? (Y/N): ");
                    quit = Console.ReadLine();
                    if (quit == "N" || quit == "n" || quit == "Y" || quit == "y")
                        break;
                    Console.WriteLine("Ошибка! Введите 'Y' или 'N'.");
                }

                if (quit == "Y" || quit == "y")
                    break;
            }
        }

        private static DateTime GetDate(string input = "")
        {
            if (input.Length == 0)
                input = Console.ReadLine();
            if (!DateTime.TryParse(input, out DateTime date))
                throw new FormatException("Ошибка! Неверный формат даты.");
            return date;
        }

        private static void Calculate(DateTime startDate, DateTime finishDate)
        {
            DateTime today = DateTime.Today;

            if (DateTime.Compare(startDate, today) > 0)
            {
                var leftBeforeStart = startDate.Subtract(today);
                Console.WriteLine($"\nСлужба начнется через {leftBeforeStart.TotalDays} дней.");
                return;
            }

            var total = finishDate.Subtract(startDate);
            var leftBeforeFinish = finishDate.Subtract(today);

            int leftDays = (leftBeforeFinish.TotalDays < 0) ? 0 : Convert.ToInt32(leftBeforeFinish.TotalDays);
            double percent = 100 / (total.TotalDays + 1) * (total.TotalDays + 1 - leftBeforeFinish.TotalDays);
            percent = (percent > 100) ? 100 : Math.Round(percent, 2);

            Console.WriteLine($"Сегодня:\t\t{today.ToString("dd.MM.yyyy")}");
            Console.WriteLine($"\nВсего дней:\t\t{total.TotalDays + 1}"); // день ДМБ также считается
            Console.WriteLine($"Осталось дней:\t\t{leftDays}");
            Console.WriteLine($"Пройдено:\t\t{percent} %");

            if (leftBeforeFinish.TotalDays == 0)
                Console.WriteLine("\nДМБ сегодня!");

            if (leftBeforeFinish.TotalDays < 0)
                Console.WriteLine($"\nСлужба завершена {-leftBeforeFinish.TotalDays} дней назад!");
        }
    }
}