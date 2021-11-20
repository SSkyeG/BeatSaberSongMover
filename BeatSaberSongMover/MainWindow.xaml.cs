using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Compression;
using System.IO;

namespace BeatSaberSongMover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string m_ToLoc = "";
        private string m_fromLoc = "";
        private string beatSaberLoc = "F:\\Program Files(x86)\\Steam\\steamapps\\common\\Beat Saber\\Beat Saber_Data\\CustomLevels";

        public MainWindow()
        {
            InitializeComponent();


            //initialize from path
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string newFrom = System.IO.Path.Combine(path, "Beat_Saber_Map_Zip_Files");
            Directory.CreateDirectory(newFrom);

            FromLoc.Text = newFrom;

            if (Directory.Exists(beatSaberLoc))
            {
                ToLoc.Text = beatSaberLoc;
            }

        }


        private void DoItButton_Click(object sender, RoutedEventArgs e)
        {
            m_ToLoc = ToLoc.Text;
            m_fromLoc = FromLoc.Text;

            m_ToLoc = m_ToLoc.Trim('"');
            m_fromLoc = m_fromLoc.Trim('"');

            if (!Directory.Exists(m_ToLoc))
            {
                MessageBox.Show("no");
                return;
            }
            if (!Directory.Exists(m_fromLoc))
            {
                MessageBox.Show("still no");
                return;
            }

            string[] dirs = Directory.GetFiles(m_fromLoc);

            foreach (string folder in dirs)
            {
                int lastSlash = folder.LastIndexOf("\\") + 1;
                int lastDot = folder.LastIndexOf(".") - 1;
                string newFolder = folder.Substring(lastSlash, folder.Length - lastSlash - (folder.Length - lastDot));
                string toPath = System.IO.Path.Combine(m_ToLoc, newFolder);
                try
                {
                    ZipFile.ExtractToDirectory(folder, toPath);
                    File.Delete(folder);
                }
                catch 
                {
                    MessageBox.Show("something went wrong when trying to extract or delete " + newFolder + "\ncheck to see if folder already exists");
                }
            }

        }

    }
}
