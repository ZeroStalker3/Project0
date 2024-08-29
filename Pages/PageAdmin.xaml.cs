﻿using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Project0.ClassHelper;
using Project0.DataBase;
using Project0.PagesSecondaires;
using System;
using System.Collections.Generic;
using System.IO;
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                var incidentRecords = OdbConnecHelper.entObj.IncidentRecord.ToList();
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var record in incidentRecords)
                    {
                        writer.WriteLine($"Номер инцидента: {record.Id}");
                        writer.WriteLine($"Дата: {record.Date}");
                        writer.WriteLine($"Описание: {record.Description}");
                        writer.WriteLine($"Сотрудник: {record.Employee?.Name ?? "Неизвестен"}");
                        writer.WriteLine("-----------------------------------");
                    }
                }

                MessageBox.Show("Данные успешно экспортированы в текстовый файл.");
            }
        }

        private void SaveDataToPdf(string filePath)
        {
            var incidentRecords = OdbConnecHelper.entObj.IncidentRecord.ToList();

            // Создаем новый PDF-документ
            PdfDocument pdfDoc = new PdfDocument();
            pdfDoc.Info.Title = "Incident Records";

            // Создаем страницу в документе
            PdfPage pdfPage = pdfDoc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

            // Устанавливаем шрифты
            XFont titleFont = new XFont("Verdana", 16);
            XFont headerFont = new XFont("Verdana", 12);
            XFont textFont = new XFont("Verdana", 10);

            // Заголовок документа
            gfx.DrawString("Incident Records", titleFont, XBrushes.Black,
                new XRect(0, 0, pdfPage.Width, 50),
                XStringFormats.TopCenter);

            // Отступ от заголовка
            double yPosition = 60;

            // Создаем таблицу для данных
            double[] columnWidths = { 40, 200, 100, 100 }; // ширина колонок
            double tableWidth = columnWidths.Sum() + 20; // общая ширина таблицы
            double xStart = (pdfPage.Width - tableWidth) / 2; // центрирование таблицы на странице

            // Заголовки колонок
            string[] headers = { "Id", "Description", "Date", "Employee Id" };

            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XPens.Black, xStart, yPosition, columnWidths[i], 20);
                gfx.DrawString(headers[i], headerFont, XBrushes.Black,
                    new XRect(xStart + 5, yPosition + 5, columnWidths[i], 20),
                    XStringFormats.TopLeft);
                xStart += columnWidths[i];
            }

            yPosition += 20;
            xStart = (pdfPage.Width - tableWidth) / 2; // сброс позиции x

            // Добавляем записи в таблицу
            foreach (var record in incidentRecords)
            {
                gfx.DrawRectangle(XPens.Black, xStart, yPosition, columnWidths[0], 20);
                gfx.DrawString(record.Id.ToString(), textFont, XBrushes.Black,
                    new XRect(xStart + 5, yPosition + 5, columnWidths[0], 20),
                    XStringFormats.TopLeft);

                gfx.DrawRectangle(XPens.Black, xStart + columnWidths[0], yPosition, columnWidths[1], 20);
                gfx.DrawString(record.Description, textFont, XBrushes.Black,
                    new XRect(xStart + columnWidths[0] + 5, yPosition + 5, columnWidths[1], 20),
                    XStringFormats.TopLeft);

                gfx.DrawRectangle(XPens.Black, xStart + columnWidths[0] + columnWidths[1], yPosition, columnWidths[2], 20);

                // Преобразование строки в DateTime и форматирование
                DateTime date;
                if (DateTime.TryParse(record.Date, out date))
                {
                    gfx.DrawString(date.ToShortDateString(), textFont, XBrushes.Black,
                        new XRect(xStart + columnWidths[0] + columnWidths[1] + 5, yPosition + 5, columnWidths[2], 20),
                        XStringFormats.TopLeft);
                }
                else
                {
                    // Если преобразование не удалось, оставляем оригинальную строку
                    gfx.DrawString(record.Date, textFont, XBrushes.Black,
                        new XRect(xStart + columnWidths[0] + columnWidths[1] + 5, yPosition + 5, columnWidths[2], 20),
                        XStringFormats.TopLeft);
                }

                gfx.DrawRectangle(XPens.Black, xStart + columnWidths[0] + columnWidths[1] + columnWidths[2], yPosition, columnWidths[3], 20);
                gfx.DrawString(record.Id_Employee.ToString(), textFont, XBrushes.Black,
                    new XRect(xStart + columnWidths[0] + columnWidths[1] + columnWidths[2] + 5, yPosition + 5, columnWidths[3], 20),
                    XStringFormats.TopLeft);

                yPosition += 20;

                // Добавляем новую страницу, если не хватает места на текущей
                if (yPosition > pdfPage.Height - 40)
                {
                    pdfPage = pdfDoc.AddPage();
                    gfx = XGraphics.FromPdfPage(pdfPage);
                    yPosition = 40; // обновляем позицию для новой страницы
                }
            }

            // Сохраняем документ в файл
            pdfDoc.Save(filePath);
            pdfDoc.Close();
        }

        private void SavePDF_Click(object sender, RoutedEventArgs e)
        {
            // Открытие диалога для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.FileName = "IncidentRecords.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Вызов метода сохранения данных в PDF
                SaveDataToPdf(saveFileDialog.FileName);
                MessageBox.Show("Данные успешно сохранены в PDF.");
            }
        }
    }
}
