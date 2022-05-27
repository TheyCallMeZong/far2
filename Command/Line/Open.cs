using System.Diagnostics;

namespace Far.Command.Line
{
    /// <summary>
    /// откртиые txt файлов
    /// </summary>
    public class Open : ICommand<string>
    {
        /// <summary>
        /// массив, хранящий команду и путь
        /// </summary>
        private string[] text;

        public bool CanExecute(string item)
        {
            text = item.Split(' ');
            return text[0] == "open";
        }

        public bool Execute()
        {
            View view = View.GetInstance();

            string pathe = GetPath(view);
            string t = text[1];

            for (int i = 2; i < text.Length; i++)
            {
                t = string.Concat(t, " ");
                t = string.Concat(t, text[i]);
            }

            if (File.Exists(pathe + "\\" + t) && GetExt(t) == ".txt")
            {
                Process process = new();
                process.StartInfo.FileName = @"C:\Windows\System32\notepad.exe";
                process.StartInfo.Arguments = pathe + "\\" + text[1];
                process.Start();
            }
            else if (File.Exists(pathe + "\\" + t) && GetExt(t) == ".docx")
            {
                Process process = new();
                var path = string.Concat(pathe, "\\");
                path = string.Concat(path, t);
                process.StartInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE";
                process.StartInfo.Arguments = "\"" + path + "\"";
                process.Start();
            }
            else if (File.Exists(pathe + "\\" + t) && GetExt(t) == ".xlsx")
            {
                Process process = new();
                var path = string.Concat(pathe, "\\");
                path = string.Concat(path, t);
                process.StartInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE";
                process.StartInfo.Arguments = "\"" + path + "\"";
                process.Start();
            }
            else if (File.Exists(pathe + "\\" + t) && GetExt(t) == ".pptx")
            {
                Process process = new();
                var path = string.Concat(pathe, "\\");
                path = string.Concat(path, t);
                process.StartInfo.FileName = @"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.EXE";
                process.StartInfo.Arguments = "\"" + path + "\"";
                process.Start();
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

        private string GetExt(string filename)
        {
            string ext = filename.Substring(filename.LastIndexOf('.'));
            return ext;
        }
    }
}
