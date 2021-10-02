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

namespace RegistrationCourse_Gracheva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities context;
        public MainWindow()
        {
            InitializeComponent();
            context = new Entities();
            ShowTable();
        }

        private void ShowTable()
        {
            DataGridRegistrations.ItemsSource = context.CourseRegistration.ToList();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = DataGridRegistrations.SelectedItem as CourseRegistration;
            if (row == null)
            {
                MessageBox.Show("Select row for deleting at first");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить эту строку данных?", "Подтверждение удаления",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context.CourseRegistration.Remove(row);
                context.SaveChanges();
                ShowTable();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newRegistration = new CourseRegistration();
            context.CourseRegistration.Add(newRegistration);
            var win = new Window1(context, newRegistration);
            win.ShowDialog();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btnEdit = sender as Button;
            var currentRegistration = btnEdit.DataContext as CourseRegistration;
            var win = new Window1(context, currentRegistration);
            win.ShowDialog();
        }
    }
}
