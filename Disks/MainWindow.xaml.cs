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
        public List<string> allCompanies = new List<string>();
        public List<string> allTypes = new List<string>();
        private bool isGoingToAdd;
        private Disk selectedDisk;
        public static string selectedCompany;
        public static string selectedType;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void InfoDiskForm_Loaded(object sender, EventArgs e)
        {
            OpenDBFile();
            Resize(400, 475);
            disksGroupBox.Visibility = Visibility.Hidden;
            selDiskGroupBox.Visibility = Visibility.Hidden;

        }
        private void OpenDBFile()
        {
            try
            {
                disks = DBConnection.GetInstance().GetAllDisks();
                DisksDG.ItemsSource = disks;
            }
            catch (Exception ex)
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
            selDiskGroupBox.Visibility = Visibility.Hidden;
            try
            {
                DisksDG.ItemsSource = null;
                disks.Clear();
            }
            catch (Exception ex)
            {
                ErrorShow(ex, "", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            OpenDBFile();
            disksGroupBox.Visibility = Visibility.Hidden;
            companyDiskList.Text = "";
            typeDiskList.Text = "";
            Resize(400, 475);
        }

        private void SelectYMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selectedType = "";
            selDiskGroupBox.Visibility = Visibility.Visible;
            typeDiskList.Visibility = Visibility.Hidden;
            Resize(this.Height + 500, 1000);
            allCompanies = DBConnection.GetInstance().SelectAllCompanies();
            companyDiskList.ItemsSource = allCompanies;
        }

        private void SelectXYMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selDiskGroupBox.Visibility = Visibility.Visible;
            typeDiskList.Visibility = Visibility.Visible;
            Resize(this.Height + 500, 1000);
            allCompanies = DBConnection.GetInstance().SelectAllCompanies();
            companyDiskList.ItemsSource = allCompanies;
            allTypes = DBConnection.GetInstance().SelectAllTypes();
            typeDiskList.ItemsSource = allTypes;
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

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dB = DBConnection.GetInstance();
            string code = codeDiskTextBox.Text;
            string company = companyDiskTextBox.Text;
            string title = titleDiskTextBox.Text;
            string type = typeDiskTextBox.Text;
            int releaseYear;
            if (!int.TryParse(releaseYearTextBox.Text, out releaseYear))
            {
                ErrorShow(new Exception(), "Невозможно конвертировать число в целочисленный тип", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (isGoingToAdd)
            {
                Disk disk = new Disk(dB.GetMaxId() + 1, code, title, company, releaseYear, type);
                DisksDG.ItemsSource = null;
                disks.Add(disk);
                DisksDG.ItemsSource = disks;
                dB.Add(disk);
            }
            else
            {
                DisksDG.ItemsSource = null;
                selectedDisk.code = code;
                selectedDisk.company = company;
                selectedDisk.title = title;
                selectedDisk.releaseYear = releaseYear;
                selectedDisk.type = type;
                DisksDG.ItemsSource = disks;
                dB.Update(selectedDisk);
            }
            
        }

        private void saveSelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (typeDiskList.Visibility == Visibility.Visible)
            {
                new ConvertDataInDoc().ConvertFlightListInDoc(SelectData.SelectY(disks, selectedCompany), SelectData.SelectXY(disks, selectedCompany, selectedType));
            }
            else
            {
                new ConvertDataInDoc().ConvertFlightListInDoc(SelectData.SelectY(disks, selectedCompany), new List<Disk>());
            }
        }

        private void selBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedCompany = (string)companyDiskList.SelectedItem;
            if (companyDiskList == null)
            {
                ErrorShow(new Exception(), "Оберіть виробника", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            selectedType = typeDiskList.Text;
            if (typeDiskList.Visibility == Visibility.Hidden)
            {
                DisksDG.ItemsSource = SelectData.SelectY(disks, selectedCompany);
            }
            else
            {
                DisksDG.ItemsSource = SelectData.SelectXY(disks, selectedCompany, selectedType);
            }
        }

        private void DisksDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DisksDG.Items.Count == 0)
            {
                return;
            }
            selectedDisk = (Disk)DisksDG.SelectedItem;
            codeDiskTextBox.Text = selectedDisk.code;
            titleDiskTextBox.Text = selectedDisk.title;
            companyDiskTextBox.Text = selectedDisk.company;
            releaseYearTextBox.Text = selectedDisk.releaseYear.ToString();
            typeDiskTextBox.Text = selectedDisk.type;
        }
    }
}
