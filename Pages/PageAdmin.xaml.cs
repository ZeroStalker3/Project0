using Project0.ClassHelper;
using Project0.DataBase;
using Project0.PagesSecondaires;
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
using System.Windows.Threading;

namespace Project0.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {

        List<IncidentRecord> _incidentRecords = new List<IncidentRecord>();

        public PageAdmin()
        {
            InitializeComponent();

            SetTimer();
            IncidentListBox.ItemsSource = OdbConnecHelper.entObj.IncidentRecord.ToList();
        }

        //Set and start the timer
        private void SetTimer()
        { 
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            IncidentListBox.ItemsSource = OdbConnecHelper.entObj.IncidentRecord.ToList();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new IncidentLogger());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text.ToLower();

            var filteredRecords = OdbConnecHelper.entObj.IncidentRecord
                .Where(r => r.Description.ToLower().Contains(query) ||
                            r.Employee.Name.ToLower().Contains(query)) // Поиск по имени сотрудника
                .ToList();

            IncidentListBox.ItemsSource = filteredRecords;
        }

        private void FilterByDateButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterDate.SelectedDate.HasValue)
            {
                string data = FilterDate.Text;
                //MessageBox.Show(data);
                IncidentListBox.ItemsSource = OdbConnecHelper.entObj.IncidentRecord.Where
                    (r => r.Date == data).ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выбер5ите дату для фильтрации.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void DeleteIncidentButton_Click(object sender, RoutedEventArgs e)
        {
            if (IncidentListBox.SelectedItem != null)
            {
                // Найти и удалить выбранную запись

                IncidentRecord recordToDelete = IncidentListBox.SelectedItem as IncidentRecord;
                OdbConnecHelper.entObj.IncidentRecord.Remove(recordToDelete);

                //string selectedItem = IncidentListBox.SelectedItem.ToString();
                //IncidentRecord recordToDelete = OdbConnecHelper.entObj.IncidentRecord
                //    .FirstOrDefault(r => r.ToString() == selectedItem);

                //Student studentObj = GridList.SelectedItems[i] as Student;
                //OdbConnectHelper.entObj.Student.Remove(studentObj);

                if (recordToDelete != null)
                {
                    _incidentRecords.Remove(recordToDelete);
                    OdbConnecHelper.entObj.SaveChangesAsync();
                    MessageBox.Show("Данный Инцидент удалён", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    IncidentListBox.ItemsSource = OdbConnecHelper.entObj.IncidentRecord.ToList();
                }
            }
        }

        private void EditIncidentButton_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageEditIncident((sender as Button).DataContext as IncidentRecord));
            // FrameApp.frmObj.Navigate(new PageEditEvalStudent((sender as Button).DataContext as Student));

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // Очистить текстовое поле поиска
            if (SearchTextBox.Text != null)
            {
                SearchTextBox.Text = string.Empty;
            }

            if (FilterDate.Text != null)
            {
                FilterDate.Text = string.Empty;
            }

            // Сбросить фильтрацию и показать все записи
            IncidentListBox.ItemsSource = OdbConnecHelper.entObj.IncidentRecord.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
