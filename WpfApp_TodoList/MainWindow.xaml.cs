using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (List.Items.Count > 0)
            {
                DeleteButton.IsEnabled = true;
            }
        }

        private void Upload_Button(object sender, RoutedEventArgs e)
        {
            string newData = Data.Text;
            List.Items.Add(newData);
            newData = string.Empty;

            if (List.Items.Contains(newData))
            {
                MessageBox.Show("You already have this on the list!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(newData))
            {
                MessageBox.Show("You don't have any data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            List.Items.Clear();
        }
    }
}