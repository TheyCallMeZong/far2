﻿namespace Far
{
    /// <summary>
    /// отрисовка форм с разными сообщениями
    /// </summary>
    public static class FormWithMessage
    {
        /// <summary>
        /// отрисовка панели для сообщений
        /// </summary>
        /// <param name="ConsoleWidht"></param>
        /// <param name="ConsoleHeight"></param>
        public static void Show(int ConsoleWidht, int ConsoleHeight)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            HorizontalLine hl = new HorizontalLine();
            hl.DrawLine(ConsoleWidht / 2 - ConsoleWidht / 4, ConsoleWidht / 2 + ConsoleWidht / 4,
                ConsoleHeight / 2 - ConsoleHeight / 4, '*');
            hl.DrawLine(ConsoleWidht / 2 - ConsoleWidht / 4, ConsoleWidht / 2 + ConsoleWidht / 4,
                ConsoleHeight / 2 + ConsoleHeight / 4, '*');

            VerticalLine vl = new VerticalLine();
            vl.DrawLine(ConsoleHeight / 2 - ConsoleHeight / 4 + 1, ConsoleHeight / 2 + ConsoleHeight / 4,
                ConsoleWidht / 2 - ConsoleWidht / 4, '|');
            vl.DrawLine(ConsoleHeight / 2 - ConsoleHeight / 4 + 1, ConsoleHeight / 2 + ConsoleHeight / 4,
                ConsoleWidht / 2 + ConsoleWidht / 4 - 1, '|');

            for (int i = 2; i < ConsoleHeight / 2; i++)
            {
                Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 4 + 1, i + ConsoleHeight / 4);
                Console.Write(string.Concat(Enumerable.Repeat(' ', ConsoleWidht / 2 - 2)));
            }
        }

        /// <summary>
        /// форма с помощью
        /// </summary>
        /// <param name="ConsoleWidht"></param>
        /// <param name="ConsoleHeight"></param>
        public static void ShowHelpMessage(int ConsoleWidht, int ConsoleHeight, string[] text)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            for (int i = 0; i < text.Length; i++)
            {
                Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 4 + 2,
                    1 + i + (ConsoleHeight / 2 - ConsoleHeight / 4));
                Console.WriteLine(text[i]);
            }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// отрисовка формы с текстом
        /// </summary>
        /// <param name="ConsoleWidht"></param>
        /// <param name="ConsoleHeight"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ShowMessage(int ConsoleWidht, int ConsoleHeight, string text)
        {
            Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 8, ConsoleHeight / 2 - ConsoleHeight / 8);
            Console.WriteLine(text);
            Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 8, ConsoleHeight / 2 - ConsoleHeight / 8 + 1);
            Console.CursorVisible = true;
            var result = Console.ReadLine();

            Console.CursorVisible = false;
            return result;
        }

        /// <summary>
        /// отрисовка окна с выбором
        /// </summary>
        /// <param name="ConsoleWidht"></param>
        /// <param name="ConsoleHeight"></param>
        public static void ShowYN(int ConsoleWidht, int ConsoleHeight, string text)
        {
            Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 8, ConsoleHeight / 2 - ConsoleHeight / 8);
            Console.Write(text);
            Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 8, ConsoleHeight / 2 - ConsoleHeight / 8 + 1);
            Console.Write("Y - Yes, N - No");
        }
    }
}
