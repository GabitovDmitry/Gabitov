namespace sem_1_lab_01_server_monitoring
{
    internal class Program
    {
        const int Width = 52; // внутренняя ширина карточки

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Сбор данных о состоянии игрового сервера ===\n");

            // --- Строковые данные ---
            Console.Write("Имя сервера: ");
            string serverName = Console.ReadLine();

            Console.Write("Игровой мир / регион: ");
            string region = Console.ReadLine();

            // --- Целые числа ---
            Console.Write("Максимум игроков: ");
            int maxPlayers = Convert.ToInt32(Console.ReadLine());

            Console.Write("Игроков онлайн: ");
            int playersOnline = Convert.ToInt32(Console.ReadLine());

            Console.Write("Задержка сети (мс): ");
            int ping = Convert.ToInt32(Console.ReadLine());

            Console.Write("Время работы без перезапуска (часов): ");
            int uptimeHours = Convert.ToInt32(Console.ReadLine());

            // --- Вещественные числа ---
            Console.Write("Загрузка CPU (%): ");
            double cpuLoad = Convert.ToDouble(Console.ReadLine());

            Console.Write("Использовано RAM (ГБ): ");
            double ramUsed = Convert.ToDouble(Console.ReadLine());

            Console.Write("Всего RAM (ГБ): ");
            double ramTotal = Convert.ToDouble(Console.ReadLine());

            Console.Write("Свободно на диске (%): ");
            double diskFree = Convert.ToDouble(Console.ReadLine());

            // --- Логическое значение ---
            Console.Write("Режим тех. обслуживания включён? (yes/no): ");
            string maintInput = Console.ReadLine().Trim().ToLower();
            bool maintenance = maintInput == "да" || maintInput == "yes" || maintInput == "y";

            // --- Вычисляемые показатели ---
            double slotLoad = maxPlayers > 0 ? (double)playersOnline / maxPlayers * 100 : 0;
            double ramLoad = ramTotal > 0 ? ramUsed / ramTotal * 100 : 0;
            int freeSlots = maxPlayers - playersOnline;

            // --- Логические проверки ---
            bool cpuOk = cpuLoad < 80;
            bool ramOk = ramLoad < 85;
            bool pingOk = ping < 100;
            bool diskOk = diskFree > 15;
            bool slotsOk = slotLoad < 95;
            bool allOk = cpuOk && ramOk && pingOk && diskOk && slotsOk && !maintenance;

            // --- Вывод ---
            Console.WriteLine();

            Top();
            Row("           СВОДКА О СОСТОЯНИИ СЕРВЕРА");
            Mid();
            Row("  Сервер:            " + serverName);
            Row("  Регион:            " + region);
            Row("  Тех. обслуживание: " + (maintenance ? "ВКЛЮЧЕНО" : "выключено"));
            Mid();
            Row("                    ЗАГРУЗКА");
            Mid();
            Row("  Игроки:            " + playersOnline + " / " + maxPlayers);
            Row("  Заполненность:     " + slotLoad.ToString("F1") + " %");
            Row("  Свободных мест:    " + freeSlots);
            Row("  CPU:               " + cpuLoad.ToString("F1") + " %");
            Row("  RAM:               " + ramUsed + " / " + ramTotal + " ГБ");
            Row("  RAM загрузка:      " + ramLoad.ToString("F1") + " %");
            Row("  Диск свободно:     " + diskFree.ToString("F1") + " %");
            Mid();
            Row("                    СЕТЬ");
            Mid();
            Row("  Ping:              " + ping + " мс");
            Row("  Uptime:            " + uptimeHours + " ч");
            Mid();
            Row("                   ДИАГНОСТИКА");
            Mid();
            Row("  CPU:      " + Mark(cpuOk));
            Row("  RAM:      " + Mark(ramOk));
            Row("  Ping:     " + Mark(pingOk));
            Row("  Диск:     " + Mark(diskOk));
            Row("  Слоты:    " + Mark(slotsOk));
            Mid();
            Row("  " + (allOk ? "СЕРВЕР ГОТОВ ПРИНИМАТЬ ИГРОКОВ"
                              : "ТРЕБУЕТСЯ ВНИМАНИЕ АДМИНИСТРАТОРА"));
            Bot();
        }

        static void Top() => Console.WriteLine("╔" + new string('═', Width) + "╗");
        static void Mid() => Console.WriteLine("╠" + new string('═', Width) + "╣");
        static void Bot() => Console.WriteLine("╚" + new string('═', Width) + "╝");

        static void Row(string text)
        {
            if (text.Length > Width) text = text.Substring(0, Width);
            Console.WriteLine("║" + text.PadRight(Width) + "║");
        }

        static string Mark(bool ok) => ok ? "OK" : "!!! превышен порог";
    }
}
