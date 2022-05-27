﻿using System.Runtime.InteropServices;

namespace Far
{
    public class View
    {
        /// <summary>
        /// Отступ для печати пути
        /// </summary>
        public int OffsetForPath { get; set; }

        /// <summary>
        /// Отуступ для печати файлов и директорий
        /// </summary>
        public int OffsetForFileAndDir { get; set; }

        /// <summary>
        /// Ширина консоли
        /// </summary>
        public int ConsoleWidht { get; set; }

        /// <summary>
        /// Высота консоли
        /// </summary>
        public int ConsoleHeight { get; set; }

        /// <summary>
        /// Отступ курсора относительно окна панели при скролинге для левой панели
        /// </summary>
        public int CursorOffsetOnLeftPanel { get; set; }

        /// <summary>
        /// Абсолютное положение курсора для левой панели
        /// </summary>
        public int AbsolutleCursorOffseOnLeftPanel { get; set; }

        /// <summary>
        /// Абсолютное положение курсора для правой панели
        /// </summary>
        public int AbsolutleCursorOffseOnRightPanel { get; set; }

        /// <summary>
        /// Отступ курсора относительно окна панели при скролинге для правой панели
        /// </summary>
        public int CursorOffsetOnRightPanel { get; set; }

        /// <summary>
        /// Все файлы и директории текущей папки левой панели
        /// </summary>
        public List<Files> FilesAndDirectoriesOnLeftPanel { get; set; }

        /// <summary>
        /// Все файлы и директории текущей папки правой панели
        /// </summary>
        public List<Files> FilesAndDirectoriesOnRightPanel { get; set; }

        /// <summary>
        /// Диски на левой панели
        /// </summary>
        public List<DriveInfo> DriversOnLeftPanel { get; set; }

        /// <summary>
        /// Диски на правой панели
        /// </summary>
        public List<DriveInfo> DriversOnRightPanel { get; set; }

        /// <summary>
        /// какая панель исользуется 
        /// </summary>
        public FilePanel FilePanel;

        /// <summary> 
        /// страница при скролинге
        /// </summary>
        public int PageInScrollingInRightPanel { get; set; }

        /// <summary> 
        /// страница при скролинге
        /// </summary>
        public int PageInScrollingInLeftPanel { get; set; }

        /// <summary>
        /// Путь
        /// </summary>
        public string PathOnLeftPanel { get; set; }

        /// <summary>
        /// Путь
        /// </summary>
        public string PathOnRightPanel { get; set; }

        /// <summary>
        /// Экземляр одиночик
        /// </summary>
        private static View instance;

        /// <summary>
        /// Получение экземпляра одиночик
        /// </summary>
        /// <returns></returns>
        public static View GetInstance()
        {
            if (instance == null)
            {
                instance = new View();
            }

            return instance;
        }

        /// <summary>
        /// отступ для расширения и размера слева
        /// </summary>
        private int maxleft;

        /// <summary>
        /// отступ для расширения и размера слева
        /// </summary>
        private int maxright;

        /// <summary>
        /// Дескриптор окна
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Функция, которая определяет положение окна
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPosition"></param>
        /// <param name="wFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        /// какие то переменные
        /// </summary>
        private const int MF_BYCOMMAND = 0x00000000;

        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

        /// <summary>
        /// Максимальное значение для показа окна
        /// </summary>
        private const int MAXSIZE = 3;

        public string[] menu = new[]
        {
            "F1 Help",
            "F2 CreateFile",
            "F3 Edit",
            "F4 Rename",
            "F5 Copy",
            "F6 Renmove",
            "F7 CreateDirectory",
            "F8 Delete",
            "F9 cmd",
            "F10 Exit"
        };

        /// <summary>
        /// Конструктор
        /// </summary>
        public View()
        {
            ConsoleWidht = Console.WindowWidth;
            ConsoleHeight = Console.WindowHeight;
            Console.BufferWidth = ConsoleWidht + 1;
            Console.BufferHeight = ConsoleHeight;
            OffsetForPath = 1;
            OffsetForFileAndDir = 3;
            CursorOffsetOnLeftPanel = 4;
            CursorOffsetOnRightPanel = 4;
            AbsolutleCursorOffseOnLeftPanel = 0;
            AbsolutleCursorOffseOnRightPanel = 0;
            DriversOnRightPanel = new List<DriveInfo>();
            DriversOnLeftPanel = new List<DriveInfo>();
            FilePanel = FilePanel.Left;
            PageInScrollingInRightPanel = 0;
            PageInScrollingInLeftPanel = 0;
        }

        /// <summary>
        /// Устанавливаем окно во весь экран и меняем цвет текста 
        /// </summary>
        public void SetWindow()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            ShowWindow(GetConsoleWindow(), MAXSIZE);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "FAR";
            ConsoleWidht = Console.WindowWidth;
            ConsoleHeight = Console.WindowHeight;
        }

        /// <summary>
        /// Дизайн
        /// </summary>
        public void ViewDesign()
        {
            for (int i = 0; i < ConsoleHeight; i++)
            {
                Console.Write(string.Concat(Enumerable.Repeat(' ', ConsoleWidht)));
            }

            HorizontalLine horizontal = new HorizontalLine();
            horizontal.DrawLine(0, ConsoleWidht, 0, '~');
            horizontal.DrawLine(0, ConsoleWidht, ConsoleHeight - 2, '~');
            horizontal.DrawLine(1, ConsoleWidht / 2, 2, '~');
            horizontal.DrawLine(ConsoleWidht / 2, ConsoleWidht, 2, '~');
            horizontal.DrawLine(1, ConsoleWidht / 2, ConsoleHeight - 4, '~');
            horizontal.DrawLine(ConsoleWidht / 2, ConsoleWidht, ConsoleHeight - 4, '~');
            VerticalLine verticalLine = new VerticalLine();
            verticalLine.DrawLine(0, ConsoleHeight - 1, 0, '|');
            verticalLine.DrawLine(0, ConsoleHeight - 1, ConsoleWidht - 1, '|');
            verticalLine.DrawLine(1, ConsoleHeight - 3, ConsoleWidht / 2, '|');
            SetMenu();
        }

        /// <summary>
        /// отображение меню
        /// </summary>
        public void SetMenu()
        {
            Console.SetCursorPosition(1, ConsoleHeight - 3);

            foreach (var item in menu)
            {
                Console.Write(item + "\t| ");
            }
        }

        /// <summary>
        /// Отображение всех файлов и директорий
        /// </summary>
        /// <param name="panel"></param>
        public void ShowFiles(Panel panel)
        {
            new Clear(ConsoleWidht, ConsoleHeight).ClearPanel(panel.FilePanel);
            OffsetForFileAndDir = 3;
            if (panel.FilePanel == FilePanel.Left)
            {
                FilesAndDirectoriesOnLeftPanel = panel.Files;
                var files = panel.Files.Where(x => x.Extension != null).ToList();
                if (files.Count != 0)
                {
                    maxleft = files.MaxBy(x => x.Extension.Length).Extension.Length;
                    if (maxleft < "Extension".Length)
                    {
                        maxleft = "Extension".Length;
                    }
                }

                Console.SetCursorPosition(GetLeftOffset(panel), OffsetForFileAndDir);
                Console.WriteLine("|Extension ");
                Console.SetCursorPosition(2 + maxleft + GetLeftOffset(panel), OffsetForFileAndDir);
                Console.WriteLine(" | Size");
                Console.SetCursorPosition(1, OffsetForFileAndDir++);
                Console.WriteLine("[..]");
                Console.SetCursorPosition(1, OffsetForPath);

                Console.Write(panel.Path);
                Console.SetCursorPosition(GetLeftOffset(panel), OffsetForFileAndDir);
                files = panel.Files.Skip(PageInScrollingInLeftPanel * (ConsoleHeight - 8)).ToList();
                foreach (var item in files)
                {
                    if (OffsetForFileAndDir == ConsoleHeight - 4)
                    {
                        break;
                    }

                    Console.SetCursorPosition(1, OffsetForFileAndDir++);
                    Console.Write(Substring(item.Name));
                    Console.SetCursorPosition(GetLeftOffset(panel), OffsetForFileAndDir);
                    if (!string.IsNullOrEmpty(item.Extension))
                    {
                        Console.SetCursorPosition(GetLeftOffset(panel), --OffsetForFileAndDir);
                        var size = item.Size / 8 / 1024;
                        if (size == 0 && item.Size != 0)
                        {
                            size = 1;
                        }

                        Console.Write("|" + item.Extension);
                        Console.SetCursorPosition(2 + maxleft + GetLeftOffset(panel), OffsetForFileAndDir);
                        Console.WriteLine($" | {size}KByte");

                        OffsetForFileAndDir++;
                    }
                }

                PathOnLeftPanel = panel.Path;
                DriversOnLeftPanel.Clear();
                CursorOffsetOnLeftPanel = 4;
                if (PageInScrollingInLeftPanel == 0)
                {
                    AbsolutleCursorOffseOnLeftPanel = 0;
                }
            }
            else
            {
                FilesAndDirectoriesOnRightPanel = panel.Files;
                var files = panel.Files.Where(x => x.Extension != null).ToList();
                if (files.Count != 0)
                {
                    maxright = files.MaxBy(x => x.Extension.Length).Extension.Length;
                    if (maxright < "Extension".Length)
                    {
                        maxright = "Extension".Length;
                    }
                }

                Console.SetCursorPosition(GetLeftOffset(panel), OffsetForFileAndDir);
                Console.WriteLine("|Extension ");
                Console.SetCursorPosition(2 + maxright + GetLeftOffset(panel), OffsetForFileAndDir);
                Console.WriteLine(" | Size");
                Console.SetCursorPosition(ConsoleWidht / 2 + 1, OffsetForFileAndDir++);
                Console.WriteLine("[..]");
                Console.SetCursorPosition(ConsoleWidht / 2 + 1, OffsetForPath);
                Console.Write(panel.Path);
                Console.SetCursorPosition(GetLeftOffset(panel), OffsetForFileAndDir);
                files = panel.Files.Skip(PageInScrollingInRightPanel * (ConsoleHeight - 8)).ToList();
                foreach (var item in files)
                {
                    if (OffsetForFileAndDir == ConsoleHeight - 4)
                    {
                        break;
                    }

                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, OffsetForFileAndDir++);
                    Console.Write(Substring(item.Name));
                    Console.SetCursorPosition(2 + GetLeftOffset(panel), OffsetForFileAndDir);
                    if (!string.IsNullOrEmpty(item.Extension))
                    {
                        Console.SetCursorPosition(GetLeftOffset(panel), --OffsetForFileAndDir);
                        var size = item.Size / 8 / 1024;
                        if (size == 0 && item.Size != 0)
                        {
                            size = 1;
                        }

                        Console.Write("|" + item.Extension);
                        Console.SetCursorPosition(2 + maxright + GetLeftOffset(panel), OffsetForFileAndDir);
                        Console.WriteLine($" | {size}KByte");
                        OffsetForFileAndDir++;
                    }
                }

                PathOnRightPanel = panel.Path;
                DriversOnRightPanel.Clear();
                CursorOffsetOnRightPanel = 4;
                if (PageInScrollingInRightPanel == 0)
                {
                    AbsolutleCursorOffseOnRightPanel = 0;
                }
            }

            OffsetForFileAndDir = 3;
        }

        /// <summary>
        /// Установка начального положения курсора
        /// </summary>
        /// <param name="panel"></param>
        public void SetStartCursor(FilePanel panel)
        {
            if (panel == FilePanel.Left)
            {
                if (FilesAndDirectoriesOnLeftPanel.Count == 0 && DriversOnLeftPanel.Count == 0)
                {
                    Console.SetCursorPosition(1, --CursorOffsetOnLeftPanel);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[..]");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (DriversOnLeftPanel.Count != 0)
                {
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(DriversOnLeftPanel[CursorOffsetOnLeftPanel - 3].Name);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                var item = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Name;
                Console.BackgroundColor = ConsoleColor.Cyan;
                if (FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Extension != null)
                {
                    ShowExt(FilePanel.Left);
                }

                Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                Console.WriteLine(Substring(item));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                if (FilesAndDirectoriesOnRightPanel.Count == 0 && DriversOnRightPanel.Count == 0)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, --CursorOffsetOnRightPanel);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[..]");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (DriversOnRightPanel.Count != 0)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(DriversOnRightPanel[CursorOffsetOnRightPanel - 3].Name);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                var item = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Name;
                Console.BackgroundColor = ConsoleColor.Cyan;
                if (FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Extension != null)
                {
                    ShowExt(FilePanel.Right);
                }

                Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                Console.WriteLine(Substring(item));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
        }

        /// <summary>
        /// перемещение курсора вниз
        /// </summary>
        public void MoveCusrorDown()
        {
            string item;
            if (FilePanel == FilePanel.Left)
            {
                if (PageInScrollingInLeftPanel > 0 &&
                    AbsolutleCursorOffseOnLeftPanel == FilesAndDirectoriesOnLeftPanel.Count - 1)
                {
                    return;
                }

                if (CursorOffsetOnLeftPanel == ConsoleHeight - 5)
                {
                    PageInScrollingInLeftPanel++;
                    ShowFiles(new Panel(PathOnLeftPanel, FilePanel.Left));
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    item = FilesAndDirectoriesOnLeftPanel[++AbsolutleCursorOffseOnLeftPanel].Name;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(item);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (DriversOnLeftPanel.Count != 0 && CursorOffsetOnLeftPanel != DriversOnLeftPanel.Count + 2)
                {
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    item = DriversOnLeftPanel[CursorOffsetOnLeftPanel - 3].Name;
                    Console.WriteLine(Substring(item));
                    Console.SetCursorPosition(1, ++CursorOffsetOnLeftPanel);
                    AbsolutleCursorOffseOnLeftPanel++;
                    item = DriversOnLeftPanel[CursorOffsetOnLeftPanel - 3].Name;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(Substring(item));
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (CursorOffsetOnLeftPanel == FilesAndDirectoriesOnLeftPanel.Count + 3 ||
                    CursorOffsetOnLeftPanel == DriversOnLeftPanel.Count + 2)
                {
                    return;
                }
                else if (CursorOffsetOnLeftPanel == 3)
                {
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    Console.WriteLine("[..]");
                }
                else if (CursorOffsetOnLeftPanel != 3)
                {
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    item = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Name;
                    Console.WriteLine(Substring(item));
                    ShowExt(FilePanel.Left);
                }

                if (CursorOffsetOnLeftPanel == ConsoleHeight - 5)
                {
                    return;
                }

                Console.SetCursorPosition(1, ++CursorOffsetOnLeftPanel);
                AbsolutleCursorOffseOnLeftPanel++;
                item = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Name;
                Console.BackgroundColor = ConsoleColor.Cyan;
                ShowExt(FilePanel.Left);
                Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                Console.WriteLine(Substring(item));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                if (PageInScrollingInRightPanel > 0 &&
                    AbsolutleCursorOffseOnRightPanel == FilesAndDirectoriesOnRightPanel.Count - 1)
                {
                    return;
                }

                if (CursorOffsetOnRightPanel == ConsoleHeight - 5)
                {
                    PageInScrollingInRightPanel++;
                    ShowFiles(new Panel(PathOnRightPanel, FilePanel.Right));
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    item = FilesAndDirectoriesOnRightPanel[++AbsolutleCursorOffseOnRightPanel].Name;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(item);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (DriversOnRightPanel.Count != 0 && CursorOffsetOnRightPanel != DriversOnRightPanel.Count + 2)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    item = DriversOnRightPanel[CursorOffsetOnRightPanel - 3].Name;
                    Console.WriteLine(Substring(item));
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, ++CursorOffsetOnRightPanel);
                    AbsolutleCursorOffseOnRightPanel++;
                    item = DriversOnRightPanel[CursorOffsetOnRightPanel - 3].Name;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(Substring(item));
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }
                else if (CursorOffsetOnRightPanel == FilesAndDirectoriesOnRightPanel.Count + 3 ||
                         CursorOffsetOnRightPanel == DriversOnRightPanel.Count + 2)
                {
                    return;
                }
                else if (CursorOffsetOnRightPanel == 3 && DriversOnRightPanel.Count == 0)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    Console.WriteLine("[..]");
                }
                else if (CursorOffsetOnRightPanel != 3 && DriversOnRightPanel.Count == 0)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    item = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Name;
                    Console.WriteLine(Substring(item));
                    ShowExt(FilePanel.Right);
                }

                Console.SetCursorPosition(ConsoleWidht / 2 + 1, ++CursorOffsetOnRightPanel);
                AbsolutleCursorOffseOnRightPanel++;
                item = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Name;
                Console.BackgroundColor = ConsoleColor.Cyan;
                ShowExt(FilePanel.Right);
                Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                Console.WriteLine(Substring(item));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
        }

        /// <summary>
        /// перемещение курсора вверх
        /// </summary>
        /// <param name="panel"></param>
        public void MoveCusrorUp()
        {
            string item;
            if (FilePanel == FilePanel.Left)
            {
                if (PageInScrollingInLeftPanel > 0 && CursorOffsetOnLeftPanel == 4)
                {
                    var cursor = AbsolutleCursorOffseOnLeftPanel;
                    PageInScrollingInLeftPanel--;
                    ShowFiles(new Panel(PathOnLeftPanel, FilePanel.Left));
                    AbsolutleCursorOffseOnLeftPanel = cursor;
                    item = FilesAndDirectoriesOnLeftPanel[--AbsolutleCursorOffseOnLeftPanel].Name;
                    Console.SetCursorPosition(1, ConsoleHeight - 5);
                    CursorOffsetOnLeftPanel = ConsoleHeight - 5;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(item);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (CursorOffsetOnLeftPanel == 4 && DriversOnLeftPanel.Count == 0)
                {
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    item = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Name;
                    Console.WriteLine(Substring(item));
                    ShowExt(FilePanel.Left);
                    Console.SetCursorPosition(1, --CursorOffsetOnLeftPanel);
                    AbsolutleCursorOffseOnLeftPanel--;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[..]");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }
                else if (DriversOnLeftPanel.Count != 0 && CursorOffsetOnLeftPanel != 3)
                {
                    Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                    item = DriversOnLeftPanel[CursorOffsetOnLeftPanel - 3].Name;
                    Console.WriteLine(Substring(item));
                    Console.SetCursorPosition(1, --CursorOffsetOnLeftPanel);
                    AbsolutleCursorOffseOnLeftPanel--;
                    item = DriversOnLeftPanel[CursorOffsetOnLeftPanel - 3].Name;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(Substring(item));
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }
                else if (CursorOffsetOnLeftPanel == 3 || FilesAndDirectoriesOnLeftPanel.Count == 0)
                {
                    return;
                }

                Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                item = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Name;
                ShowExt(FilePanel.Left);
                Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                Console.WriteLine(Substring(item));
                Console.SetCursorPosition(1, --CursorOffsetOnLeftPanel);
                AbsolutleCursorOffseOnLeftPanel--;
                item = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Name;
                Console.BackgroundColor = ConsoleColor.Cyan;
                ShowExt(FilePanel.Left);
                Console.SetCursorPosition(1, CursorOffsetOnLeftPanel);
                Console.WriteLine(Substring(item));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                if (PageInScrollingInRightPanel > 0 && CursorOffsetOnRightPanel == 4)
                {
                    var cursor = AbsolutleCursorOffseOnRightPanel;
                    PageInScrollingInRightPanel--;
                    ShowFiles(new Panel(PathOnRightPanel, FilePanel.Right));
                    AbsolutleCursorOffseOnRightPanel = cursor;
                    item = FilesAndDirectoriesOnRightPanel[--AbsolutleCursorOffseOnRightPanel].Name;
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, ConsoleHeight - 5);
                    CursorOffsetOnRightPanel = ConsoleHeight - 5;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(item);
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }

                if (CursorOffsetOnRightPanel == 4 && DriversOnRightPanel.Count == 0)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    item = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Name;
                    Console.WriteLine(Substring(item));
                    ShowExt(FilePanel.Right);
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, --CursorOffsetOnRightPanel);
                    AbsolutleCursorOffseOnRightPanel--;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[..]");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }
                else if (DriversOnRightPanel.Count != 0 && CursorOffsetOnRightPanel != 3)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                    item = DriversOnRightPanel[CursorOffsetOnRightPanel - 3].Name;
                    Console.WriteLine(Substring(item));
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, --CursorOffsetOnRightPanel);
                    AbsolutleCursorOffseOnRightPanel--;
                    item = DriversOnRightPanel[CursorOffsetOnRightPanel - 3].Name;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(Substring(item));
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return;
                }
                else if (CursorOffsetOnRightPanel == 3 || FilesAndDirectoriesOnRightPanel.Count == 0)
                {
                    return;
                }

                Console.SetCursorPosition(ConsoleWidht / 2 + 1, CursorOffsetOnRightPanel);
                item = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Name;
                Console.WriteLine(Substring(item));
                ShowExt(FilePanel.Right);
                Console.SetCursorPosition(ConsoleWidht / 2 + 1, --CursorOffsetOnRightPanel);
                AbsolutleCursorOffseOnRightPanel--;
                item = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Name;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Substring(item));
                ShowExt(FilePanel.Right);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
        }

        /// <summary>
        /// отображение дисков
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ShowDisk(FilePanel panel)
        {
            var p = panel;
            if (p == FilePanel.Left)
            {
                DriversOnLeftPanel.Clear();
                new Clear(ConsoleWidht, ConsoleHeight).ClearPanel(FilePanel.Left);

                var drivers = DriveInfo.GetDrives();
                foreach (var driver in drivers)
                {
                    Console.SetCursorPosition(1, OffsetForFileAndDir++);
                    Console.Write(driver.Name);
                    DriversOnLeftPanel.Add(driver);
                }

                FilesAndDirectoriesOnLeftPanel.Clear();
            }
            else
            {
                DriversOnRightPanel.Clear();
                new Clear(ConsoleWidht, ConsoleHeight).ClearPanel(FilePanel.Right);

                var drivers = DriveInfo.GetDrives();
                foreach (var driver in drivers)
                {
                    Console.SetCursorPosition(ConsoleWidht / 2 + 1, OffsetForFileAndDir++);
                    Console.Write(driver.Name);
                    DriversOnRightPanel.Add(driver);
                }

                FilesAndDirectoriesOnRightPanel.Clear();
            }

            OffsetForFileAndDir = 3;
        }

        /// <summary>
        /// вынес повторяющийся код
        /// </summary>
        /// <param name="panel"></param>
        public void ShowExt(FilePanel panel)
        {
            string ext;
            long size;
            if (panel == FilePanel.Left)
            {
                if (FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Extension != null)
                {
                    ext = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Extension;
                    size = FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Size;
                    Console.SetCursorPosition(ConsoleWidht / 2 - ConsoleWidht / 4, CursorOffsetOnLeftPanel);
                    Console.WriteLine("|" + ext);
                    size = size / 8 / 1024;
                    if (size == 0 && FilesAndDirectoriesOnLeftPanel[AbsolutleCursorOffseOnLeftPanel].Size != 0)
                    {
                        size = 1;
                    }

                    Console.SetCursorPosition(maxleft + 2 + ConsoleWidht / 2 - ConsoleWidht / 4,
                        CursorOffsetOnLeftPanel);
                    Console.WriteLine($" | {size}KByte");
                }
            }
            else
            {
                if (FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Extension != null)
                {
                    ext = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Extension;
                    size = FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Size;
                    Console.SetCursorPosition(ConsoleWidht - ConsoleWidht / 4, CursorOffsetOnRightPanel);
                    Console.WriteLine("|" + ext);
                    Console.SetCursorPosition(maxright + 2 + ConsoleWidht - ConsoleWidht / 4, CursorOffsetOnRightPanel);
                    size = size / 8 / 1024;
                    if (size == 0 && FilesAndDirectoriesOnRightPanel[AbsolutleCursorOffseOnRightPanel].Size != 0)
                    {
                        size = 1;
                    }

                    Console.WriteLine(" | " + size + "KByte");
                }
            }
        }

        /// <summary>
        /// Обрезание 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string Substring(string str)
        {
            if (str.Length > ConsoleWidht / 5)
            {
                str = str.Substring(0, ConsoleWidht / 5) + "..";
            }

            return str;
        }

        /// <summary>
        /// получение отступа для отображение расширения и размера файла
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public int GetLeftOffset(Panel panel)
        {
            if (panel.FilePanel == FilePanel.Left)
            {
                return ConsoleWidht / 2 - ConsoleWidht / 4;
            }
            else
            {
                return ConsoleWidht - ConsoleWidht / 4;
            }
        }
    }
}