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
using Project0.Pages;

namespace Project0.PagesSecondaires
{
    /// <summary>
    /// Логика взаимодействия для PageAddNewEmployee.xaml
    /// </summary>
    public partial class PageAddNewEmployee : Page
    {
        public PageAddNewEmployee()
        {
            InitializeComponent();

            FrameApp1.frmObj = FrmEmpl;
            DatagridEmployes.ItemsSource = OdbConnecHelper.entObj.Employee.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void UpdateDate_Click(object sender, RoutedEventArgs e)
        {
            DatagridEmployes.ItemsSource = OdbConnecHelper.entObj.Employee.ToList();
        }

        private void AddEmployees_Click(object sender, RoutedEventArgs e)
        {
            FrameApp1.frmObj.Navigate(new AddEmployee());
        }
    }
}
