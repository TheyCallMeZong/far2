namespace Far.Command
{
    /// <summary>
    /// Изменение имени файла
    /// </summary>
    public class Rename : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo item)
        {
            return item.Key == ConsoleKey.F4;
        }

        public bool Execute()
        {
            View view = View.GetInstance();
            FormWithMessage.Show(view.ConsoleWidht, view.ConsoleHeight);
            var newFileName =
                FormWithMessage.ShowMessage(view.ConsoleWidht, view.ConsoleHeight, "Enter new file name:");

            if (string.IsNullOrEmpty(newFileName))
            {
                Window.HideMessage();
                return false;
            }
                
            newFileName = VerifyName(newFileName);

            try
            {
                if (view.FilePanel == FilePanel.Left)
                {
                    var file = view.PathOnLeftPanel + "\\" +
                               view.FilesAndDirectoriesOnLeftPanel[view.AbsolutleCursorOffseOnLeftPanel].Name;
                    if (File.Exists(file))
                    {
                        if (!File.Exists(view.PathOnLeftPanel + "\\" + newFileName))
                        {
                            File.Move(file, view.PathOnLeftPanel + "\\" + newFileName);
                        }
                    }
                    else if (Directory.Exists(file))
                    {
                        if (!Directory.Exists(view.PathOnLeftPanel + "\\" + newFileName))
                        {
                            Directory.Move(file, view.PathOnLeftPanel + "\\" + newFileName);
                        }
                    }
                }
                else
                {
                    var file = view.PathOnRightPanel + "\\" +
                               view.FilesAndDirectoriesOnRightPanel[view.AbsolutleCursorOffseOnRightPanel].Name;
                    if (File.Exists(file))
                    {
                        if (!File.Exists(view.PathOnRightPanel + "\\" + newFileName))
                        {
                            File.Move(file, view.PathOnRightPanel + "\\" + newFileName);
                        }
                    }
                    else if (Directory.Exists(file))
                    {
                        if (!Directory.Exists(view.PathOnRightPanel + "\\" + newFileName))
                        {
                            Directory.Move(file, view.PathOnRightPanel + "\\" + newFileName);
                        }
                    }
                }
            }
            finally
            {
                Window.HideMessage();
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
