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

namespace Project0.PagesSecondaires
{
    /// <summary>
    /// Логика взаимодействия для PageEditIncident.xaml
    /// </summary>
    public partial class PageEditIncident : Page
    {
        string employees;
        int incidentId;

        public PageEditIncident(IncidentRecord incidentRecord)
        {
            InitializeComponent();
            LoadData();

            IncidentDate.Text = incidentRecord.Date;
            string Incidentdescription = incidentRecord.Description;
            int Incidentemployees = incidentRecord.Id_Employee;
            incidentId = incidentRecord.Id;


            IncidentDescription.Text = Incidentdescription;
            IncidentEmployees.SelectedValue = Incidentemployees;
            MessageBox.Show($"Сотрудник: {Incidentemployees}, Описание: {Incidentdescription}, Дата: {IncidentDate}");
        }

        public void LoadData()
        {
            IncidentEmployees.DisplayMemberPath = "Name";
            IncidentEmployees.SelectedValuePath = "Id";
            IncidentEmployees.ItemsSource = OdbConnecHelper.entObj.Employee.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void EditIncidentButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на наличие выбранной даты
            string date = IncidentDate.SelectedDate.HasValue ?
                IncidentDate.SelectedDate.Value.ToString("g") : "Дата не выбрана";
            string description = IncidentDescription.Text;

            employees = Convert.ToString(IncidentEmployees.SelectedValue);

            // Проверка на пустые значения
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(employees))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Поиск записи в базе данных по ID
            var incident = OdbConnecHelper.entObj.IncidentRecord.Find(incidentId);
            if (incident == null)
            {
                MessageBox.Show("Запись не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Обновление полей найденной записи
            incident.Date = IncidentDate.Text;
            incident.Description = description;
            incident.Id_Employee = int.Parse(employees); // employees - это ID сотрудника

            // Сохранение изменений
            try
            {
                OdbConnecHelper.entObj.SaveChanges();
                MessageBox.Show("Данные инцидента изменены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
