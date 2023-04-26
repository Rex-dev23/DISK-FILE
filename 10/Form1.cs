using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] astrLogicalDrives = System.IO.Directory.GetLogicalDrives();
            foreach (string disk in astrLogicalDrives)
                listBox1.Items.Add(disk);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выводим информацию о диске
            System.IO.DriveInfo drv = new System.IO.DriveInfo(@"d:\");
            listBox1.Items.Clear();
            listBox1.Items.Add("Диск: " + drv.Name);

            if (drv.IsReady)
            {
                listBox1.Items.Add("Тип диска: " + drv.DriveType.ToString());
                listBox1.Items.Add("Файловая система: " +
                drv.DriveFormat.ToString());
                listBox1.Items.Add("Свободное место на диске: " +
                drv.AvailableFreeSpace.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Получим список папок на диске D:
            listBox1.Items.Clear();
            string[] astrFolders = System.IO.Directory.GetDirectories(@"c:\");
            foreach (string folder in astrFolders)
                listBox1.Items.Add(folder);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Переименовываем папку MyFolder в папку NewFolder
            string oldPathString = @"C:\MyFolder";
            string newPathString = @"C:\NewFolder";
            Directory.Move(oldPathString, newPathString);
            MessageBox.Show("Папка переменована.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Попытаемся удалить папку C:\WUTEMP
                System.IO.Directory.Delete(@"c:\wutemp");
                MessageBox.Show("Папка удалена.");
            }
            catch (Exception)
            {
                MessageBox.Show("Нельзя удалить непустую папку.");
            }
            finally { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            // задаем папку верхнего уровня
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            // Заголовок в диалоговом окне
            fbd.Description = "Выберите папку";
            // Не выводим кнопку Новая папка
            fbd.ShowNewFolderButton = false;
            // Получаем папку, выбранную пользователем
            if (fbd.ShowDialog() == DialogResult.OK)
                this.Text = fbd.SelectedPath;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new
            System.IO.DirectoryInfo(@"c:\TITAN");
            listBox1.Items.Clear();
            listBox1.Items.Add("Проверка папки: " + dir.Name);
            listBox1.Items.Add("Родительская папка: " + dir.Parent.Name);
            listBox1.Items.Add("Папка существует: ");
            listBox1.Items.Add(dir.Exists.ToString());
            if (dir.Exists)
            {
                listBox1.Items.Add("Папка создана: ");
                listBox1.Items.Add(dir.CreationTime.ToString());
                listBox1.Items.Add("Папка изменена: ");
                listBox1.Items.Add(dir.LastWriteTime.ToString());
                listBox1.Items.Add("Время последнего доступа: ");
                listBox1.Items.Add(dir.LastAccessTime.ToString());
                listBox1.Items.Add("Атрибуты папки: ");
                listBox1.Items.Add(dir.Attributes.ToString());
                listBox1.Items.Add("Папка содержит: " +
                dir.GetFiles().Length.ToString() + " файла");
            }
        }

       
            static long GetDirectorySize(System.IO.DirectoryInfo directory, bool includeSubdir)
            {
                long totalSize = 0;
                // Считаем все файлы
                System.IO.FileInfo[] files = directory.GetFiles();
                foreach (System.IO.FileInfo file in files)
                {
                    totalSize += file.Length;
                }
           
                     // Считаем все подпапки
               if (includeSubdir)
                {
                    System.IO.DirectoryInfo[] dirs = directory.GetDirectories();
                    foreach (System.IO.DirectoryInfo dir in dirs)
                    {
                        totalSize += GetDirectorySize(dir, true);
                    }
                }
                return totalSize;
            }
            private void butFolderSize_Click(object sender, EventArgs e)
            {
                this.Cursor = Cursors.WaitCursor;
                System.IO.DirectoryInfo dir = new
                System.IO.DirectoryInfo(@"c:\TITAN");
                textBox1.Text = "Общий размер: " +
                GetDirectorySize(dir, true).ToString() + " байт.";
                this.Cursor = Cursors.Default;
            }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] astrFiles = System.IO.Directory.GetFiles(@"c:\");
            listBox1.Items.Add("Всего файлов: " + astrFiles.Length);
            foreach (string file in astrFiles)
                listBox1.Items.Add(file);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string[] directoryEntries =
            System.IO.Directory.GetFileSystemEntries(@"c:\windows");
            foreach (string str in directoryEntries)
            {
                listBox1.Items.Add(str);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Получим список папок, где встречается буквосочетание pro
            listBox1.Items.Clear();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"d:\");
            System.IO.DirectoryInfo[] folders = di.GetDirectories("*pro*");
            foreach (System.IO.DirectoryInfo maskdirs in folders)
                listBox1.Items.Add(maskdirs);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(@"C:\windows"))
                label1.Text = "Папка " + @"C:\Windows" + " существует";
            else
                label1.Text = "Папка не существует";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string[] astrFiles = System.IO.Directory.GetFiles(@"c:\", "*.O?");
            listBox1.Items.Add("Всего файлов: " + astrFiles.Length);
            foreach (string file in astrFiles)
                listBox1.Items.Add(file);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\test.txt"))
                label1.Text = "Файл test.txt существует";
            else
                label1.Text = "Файл test.txt не существует";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // Полный путь к файлу
            string fileNamePath = @"C:\Windows\Web\Wallpaper\Windows";
            // Имя файла с расширением
            listBox1.Items.Add(System.IO.Path.GetFileName(fileNamePath));
            // Имя файла без расширения
            listBox1.Items.Add(
            System.IO.Path.GetFileNameWithoutExtension(fileNamePath));
        }
    }
}
