﻿using System.Windows;
using System.Windows.Controls;

namespace WpfApp_TodoList
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            DeleteButton.IsEnabled = List.SelectedItems.Count > 0;
            ModifyButton.IsEnabled = List.SelectedItems.Count > 0;
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
                string updatedData = Data.Text;

                if (string.IsNullOrEmpty(updatedData))
                {
                    MessageBox.Show("Please enter data to modify", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                List.Items[List.SelectedIndex] = updatedData;
                Data.Clear();
            }
            else
            {
                MessageBox.Show("Please select data to modify", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtons();
        }
    }
}
