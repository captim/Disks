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

namespace Disks
{
    public partial class MainWindow : Window
    {
        private List<Disk> disks = new List<Disk>();
        private bool isGoingToAdd;
        private Disk selectedDisk;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void InfoDiskForm_Loaded(object sender, EventArgs e)
        {
            try
            {
                disks = DBConnection.GetInstance().GetAllDisks();
                DisksDG.ItemsSource = disks;
            } catch (Exception ex)
            {
                string errMsg = "";
                if (ex.Message == "Unable to connect to any of the specified MySQL hosts.")
                {
                    errMsg = "Підключіть веб-сервер MySQL та виконайте команду Файл-Завантажити";
                }
                else
                {
                    errMsg = "Для завантаження файлу виконайте команду Файл-Завантажити";
                }
                ErrorShow(ex, errMsg, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Resize(400, 475);
            disksGroupBox.Visibility = Visibility.Hidden;
            selDiskGroupBox.Visibility = Visibility.Hidden;

        }
        public static void ErrorShow(Exception ex, string errMsg,
            MessageBoxButton MsgBtn, MessageBoxImage MsgImg)
        {
            MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) +
                                            errMsg, "Помилка", MsgBtn, MsgImg);
        }
        public void Resize(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }
        private void LoadDataMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectYMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectXYMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            disksGroupBox.Visibility = Visibility.Visible;
            Resize(this.Height, 1000);
            isGoingToAdd = false;
            if (selectedDisk != null)
            {
                
            }
        }

        private void AddDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            disksGroupBox.Visibility = Visibility.Visible;
            Resize(this.Height, 1000);
            isGoingToAdd = true;
        }

        private void DisksDG_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dB = DBConnection.GetInstance();
            if (isGoingToAdd)
            {
                string code = codeDiskTextBox.Text;
                string company = companyDiskTextBox.Text;
                string title = titleDiskTextBox.Text;
                string type = typeDiskTextBox.Text;
                int releaseYear;
                if (!int.TryParse(releaseYearTextBox.Text, out releaseYear)) {
                    ErrorShow(new Exception(), "Невозможно конвертировать число в целочисленный тип", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Disk disk = new Disk(dB.GetMaxId() + 1, code, title, company, releaseYear, type);
                DisksDG.ItemsSource = null;
                disks.Add(disk);
                DisksDG.ItemsSource = disks;
                dB.Add(disk);

            }
            else
            {

            }
            
        }

        private void saveSelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void selBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisksDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDisk = (Disk)DisksDG.SelectedItem;
            codeDiskTextBox.Text = selectedDisk.code;
            titleDiskTextBox.Text = selectedDisk.title;
            companyDiskTextBox.Text = selectedDisk.company;
            releaseYearTextBox.Text = selectedDisk.releaseYear.ToString();
            typeDiskTextBox.Text = selectedDisk.type;
        }
    }
}
