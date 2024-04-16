using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp_TodoList
{
    public partial class MainWindow : Window
    {
        int selected;

        public MainWindow()
        {
            InitializeComponent();

            List.Items.Add("test1");
            List.Items.Add("test2");

            this.KeyDown += MainWindow_KeyDown;

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            DeleteButton.IsEnabled = List.SelectedItems.Count > 0;
            ModifyButton.IsEnabled = List.SelectedItems.Count > 0;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4 && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                e.Handled = true;
            }
        }

        private void OnChange(object sender, TextChangedEventArgs e)
        {
            if (Data.Text != "") DataPlaceholder.Visibility = Visibility.Hidden;
            else DataPlaceholder.Visibility = Visibility.Visible;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string newData = Data.Text;

            if (string.IsNullOrEmpty(newData))
            {
                MessageBox.Show("Please enter a data to add to list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (List.Items.Contains(newData))
            {
                MessageBox.Show("This data is already on the list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List.Items.Add(newData);
            Data.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItems.Count > 0)
            {
                while (List.SelectedItems.Count > 0)
                {
                    List.Items.Remove(List.SelectedItems[0]);
                }

                UpdateButtons();
            }
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (List.SelectedIndex != -1)
            {
                selected = List.SelectedIndex;

                Data.Focus();
                Data.Text = List.SelectedItem as string;

                OKButton.Visibility = Visibility.Visible;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected != -1 && !string.IsNullOrEmpty(Data.Text))
            {
                List.Items[selected] = Data.Text;

                selected = -1;

                OKButton.Visibility = Visibility.Collapsed;
            }
        }

        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtons();
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                int selected = List.SelectedIndex;
                if (selected > 0)
                {
                    object selectedItem = List.SelectedItem;
                    List.Items.RemoveAt(selected);
                    List.Items.Insert(selected - 1, selectedItem);
                    List.SelectedIndex = selected - 1;
                }
            }
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                int selected = List.SelectedIndex;
                if (selected < List.Items.Count - 1 && selected != -1)
                {
                    object selectedItem = List.SelectedItem;
                    List.Items.RemoveAt(selected);
                    List.Items.Insert(selected + 1, selectedItem);
                    List.SelectedIndex = selected + 1;
                }
            }
        }

        private void DeleteTestsButton_Click(object sender, RoutedEventArgs e)
        {
            List.Items.Clear();
            DeleteTestsButton.IsEnabled = false;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
