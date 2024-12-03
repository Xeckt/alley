using Microsoft.Win32;
using ModManager.Handler;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace ModManager
{
    public partial class MainWindow : Window 
    {
        public ObservableCollection<Mod> ModsList { get; set; } = new ObservableCollection<Mod>();

        public MainWindow() 
        {
            InitializeComponent();
            ModsList = new ObservableCollection<Mod>(CacheHandler.LoadMods());
            if (ModsList.Count > 0)
            {
                ModsDataGrid.ItemsSource = ModsList;
            }
        }

        private void AddModsButton_Click(object sender, RoutedEventArgs e) 
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Compressed Files (*.zip;*.rar;*.7z)|*.zip;*.rar;*.7z",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var filePath in openFileDialog.FileNames)
                {
                    try
                    {
                        var mod = ArchiveHandler.ExtractMod(filePath);
                        ModsList.Add(mod);
                        CacheHandler.SaveMods(ModsList);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to add mod: {ex.Message}");
                    }
                }
            }
        }

        private void RemoveModsButton_Click(object sender, RoutedEventArgs e) 
        {
            var selectedMods = ModsDataGrid.SelectedItems.Cast<Mod>().ToList();
            foreach (var mod in selectedMods)
            {
                ModsList.Remove(mod);
            }
            CacheHandler.SaveMods(ModsList);
        }

        private void ExportProfileButton_Click(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show("Export Profile feature is not implemented yet.");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings menu is not implemented yet.");

            // Open the Settings window
            //Settings settingsWindow = new Settings();
            //settingsWindow.Show();
        }
    }
}
