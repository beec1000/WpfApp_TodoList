using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp_TodoList
{
    public partial class MainWindow : Window
    {
        /*
         2 órát hiányoztam

        Dokumentáció:
        -Alap adatok meg vannak adva
        -Alt+F4 ki van kapcsolva
        -Hozzáadás, Változtatás, Törlés, Sorrendbe rakás, Fel és Le mozgatás gombok működnek
        -Copy-zás műsik listába működik

        Hibák:
        -A test adatokat törlő gomb nem jó teljesen (egyszer töröl minden Item-et a listában)
        -A Title Bar-ban minden eltünik
        -Ha az egyik item-et át változtatja egy olyanara, ami már benne van a listába, azt nem kezeli
         */

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
            if (e.Key == Key.System && e.SystemKey == Key.F4)
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

                Data.Text = string.Empty;

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
                else
                {
                    MessageBox.Show("This item is already at the top of the list.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                else
                {
                    MessageBox.Show("This item is already at the bottom of the list.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void AscendingButton_Click(object sender, RoutedEventArgs e)
        {
            var ascItems = new List<string>();
            foreach (var i in List.Items)
            {
                ascItems.Add(i.ToString());
            }

            ascItems.Sort();

            List.Items.Clear();
            foreach (var i in ascItems)
            {
                List.Items.Add(i);
            }
        }

        private void DescendingButton_Click(object sender, RoutedEventArgs e)
        {
            var descItems = new List<string>();
            foreach (var i in List.Items)
            {
                descItems.Add(i.ToString());
            }

            descItems.Sort();
            descItems.Reverse();

            List.Items.Clear();
            foreach (var i in descItems)
            {
                List.Items.Add(i);
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var tempHolder = List.SelectedItem;
            List2.Items.Add(tempHolder);
        }
    }
}
