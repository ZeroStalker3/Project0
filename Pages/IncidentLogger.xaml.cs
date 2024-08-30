using Project0.ClassHelper;
using Project0.DataBase;
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

namespace Project0.Pages
{
    /// <summary>
    /// Логика взаимодействия для IncidentLogger.xaml
    /// </summary>
    public partial class IncidentLogger : Page
    {
        string employees;
        string type;
        public IncidentLogger()
        {
            InitializeComponent();

            IncidentDate.DisplayDate = DateTime.Now;

            LoadData();
        }

        public void LoadData()
        {
            IncidentEmployees.DisplayMemberPath = "Name";
            IncidentEmployees.SelectedValuePath = "Id";
            IncidentEmployees.ItemsSource = OdbConnecHelper.entObj.Employee.ToList();

            IncidentType.DisplayMemberPath = "Name";
            IncidentType.SelectedValuePath= "Id";
            IncidentType.ItemsSource = OdbConnecHelper.entObj.IncidentType.ToList();
        }

        private void AddIncidentButton_Click(object sender, RoutedEventArgs e)
        {

            string date = IncidentDate.SelectedDate.HasValue ?
                IncidentDate.SelectedDate.Value.ToString("g") : "Дата не выбрана";
            string description = IncidentDescription.Text;

            type = Convert.ToString(IncidentType.SelectedValue);
            employees = Convert.ToString(IncidentEmployees.SelectedValue);
            //MessageBox.Show(employees); //Для проверки какой ID пользователя
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(employees) || string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int ID = OdbConnecHelper.entObj.Employee.ToString().GetHashCode();
            IncidentRecord newRecord = new IncidentRecord
            {
                Date = date,
                Description = description,
                Id_Employee = Convert.ToInt32(employees),
                Id_Type = Convert.ToInt32(type)
            };

            // Вставьте код для сохранения записи, например, в базу данных или файл
            OdbConnecHelper.entObj.IncidentRecord.Add(newRecord);
            OdbConnecHelper.entObj.SaveChangesAsync();
            MessageBox.Show("Запись добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очистка полей ввода
            IncidentDate.SelectedDate = DateTime.Now;
            IncidentDescription.Clear();
            IncidentEmployees.SelectedIndex = 0;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}
