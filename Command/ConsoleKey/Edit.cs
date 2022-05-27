using System.Diagnostics;

namespace Far.Command
{
    public class Edit : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo item)
        {
            return item.Key == ConsoleKey.F3;
        }

        public bool Execute()
        {
            View view = View.GetInstance();

            Files item = GetFiles(view);

            if (item.Extension == ".txt")
            {
                Process process = new();
                process.StartInfo.FileName = @"C:\Windows\System32\notepad.exe";
                process.StartInfo.Arguments = item.Path + "\\" + item.Name;
                process.Start();
            }
            else if (item.Extension == ".exe")
            {
                Process process = new Process();
                process.StartInfo.FileName = item.Path + "\\" + item.Name;
                process.Start();
            }
            else if (item.Extension == ".docx")
            {
                if (File.Exists(item.Path + "\\" + item.Name))
                {
                    Process process = new();
                    var path = string.Concat(item.Path, "\\");
                    path = string.Concat(path, item.Name);
                    process.StartInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE";
                    process.StartInfo.Arguments = "\"" + path + "\"";
                    process.Start();
                }
            }
            else if (item.Extension == ".xlsx")
            {
                if (File.Exists(item.Path + "\\" + item.Name))
                {
                    Process process = new();
                    var path = string.Concat(item.Path, "\\");
                    path = string.Concat(path, item.Name);
                    process.StartInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
                    process.StartInfo.Arguments = "\"" + path + "\"";
                    process.Start();
                }
            }
            else if (item.Extension == ".pptx")
            {
                if (File.Exists(item.Path + "\\" + item.Name))
                {
                    Process process = new();
                    var path = string.Concat(item.Path, "\\");
                    path = string.Concat(path, item.Name);
                    process.StartInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE";
                    process.StartInfo.Arguments = "\"" + path + "\"";
                    process.Start();
                }
            }

            return false;
        }

        private Files GetFiles(View view)
        {
            if (view.FilePanel == FilePanel.Left)
            {
                return view.FilesAndDirectoriesOnLeftPanel[view.AbsolutleCursorOffseOnLeftPanel];
            }
            return view.FilesAndDirectoriesOnRightPanel[view.AbsolutleCursorOffseOnRightPanel];
        }
    }
}
