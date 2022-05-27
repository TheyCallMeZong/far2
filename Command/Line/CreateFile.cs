﻿namespace Far.Command.Line
{
    /// <summary>
    /// создание файла
    /// </summary>
    public class CreateFile : ICommand<string>
    {
        /// <summary>
        /// масив, хранящий команду и название файла
        /// </summary>
        private string[] text;

        public bool CanExecute(string item)
        {
            text = item.Split(' ');
            return text[0].ToLower() == "mkf" && text.Length == 2 && !string.IsNullOrEmpty(text[1]);
        }

        public bool Execute()
        {
            View view = View.GetInstance();
            text[1] = VerifyName(text[1]);
            try
            {
                if (view.FilePanel == FilePanel.Left)
                {
                    var e = Directory.GetFiles(view.PathOnLeftPanel, "*" + text[1]);
                    var file = File.Create(File.Exists(view.PathOnLeftPanel + "\\" + text[1])
                        ? view.PathOnLeftPanel + "\\" + $"({e.Length + 1})" + text[1]
                        : view.PathOnLeftPanel + "\\" + text[1]);
                    new Clear(view.ConsoleWidht, view.ConsoleHeight).ClearPanel(FilePanel.Left);
                    view.ShowFiles(new Panel(view.PathOnLeftPanel, FilePanel.Left));
                    file.Close();
                }
                else
                {
                    var e = Directory.GetFiles(view.PathOnRightPanel, "*" + text[1]);
                    var file = File.Create(File.Exists(view.PathOnRightPanel + "\\" + text[1])
                        ? view.PathOnRightPanel + "\\" + $"({e.Length + 1})" + text[1]
                        : view.PathOnRightPanel + "\\" + text[1]);
                    new Clear(view.ConsoleWidht, view.ConsoleHeight).ClearPanel(FilePanel.Right);
                    view.ShowFiles(new Panel(view.PathOnRightPanel, FilePanel.Right));
                    file.Close();
                }

                CommandLine.Text = "good";
                CommandLine.BackColor = ConsoleColor.Green;
            }
            catch
            {
                CommandLine.Text = "error";
                CommandLine.BackColor = ConsoleColor.Red;
            }

            return false;
        }

        private string VerifyName(string filename)
        {
            try
            {
                return filename = filename.Remove(0, filename.Length - 3) == "txt" ? filename : filename + ".txt";
            }
            catch
            {
                return filename + ".txt";
            }
        }
    }
}
