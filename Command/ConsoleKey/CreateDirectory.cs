namespace Far.Command
{
    /// <summary>
    /// Создание директории
    /// </summary>
    public class CreateDirectory : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo item)
        {
            return item.Key == ConsoleKey.F7;
        }

        public bool Execute()
        {
            View view = View.GetInstance();
            FormWithMessage.Show(view.ConsoleWidht, view.ConsoleHeight);
            string name = FormWithMessage.ShowMessage(view.ConsoleWidht, view.ConsoleHeight, "Enter directory name: ");
            if (string.IsNullOrEmpty(name))
            {
                Window.HideMessage();
                return false;
            }

            string path = GetPath(view);

            try
            {
                var e = Directory.GetDirectories(path, "*" + name);
                Directory.CreateDirectory(Directory.Exists(path + "\\" + name)
                    ? path + "\\" + $"({e.Length + 1})" + name
                    : path + "\\" + name);
            }
            finally
            {
                Window.HideMessage();
            }

            return false;
        }

        private string GetPath(View view)
        {
            if (view.FilePanel == FilePanel.Left)
            {
                return view.PathOnLeftPanel;
            }
            return view.PathOnRightPanel;
        }
    }
}
