using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;


namespace ModManager
{
    public partial class MainWindow : Window {
        public ObservableCollection<Mod> Mods { get; set; } = new ObservableCollection<Mod>();

        public MainWindow() {
            InitializeComponent();
            Mods = new ObservableCollection<Mod>(JsonCacheManager.LoadMods());
            ModsDataGrid.ItemsSource = Mods;
        }


        private void AddModsButton_Click(object sender, RoutedEventArgs e) {
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
                        Mods.Add(mod);
                        JsonCacheManager.SaveMods(Mods);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to add mod: {ex.Message}");
                    }
                }
            }
        }

        private void RemoveModsButton_Click(object sender, RoutedEventArgs e) {
            var selectedMods = ModsDataGrid.SelectedItems.Cast<Mod>().ToList();
            foreach (var mod in selectedMods)
            {
                Mods.Remove(mod);
            }
            JsonCacheManager.SaveMods(Mods);
        }

        private void ExportProfileButton_Click(object sender, RoutedEventArgs e) {
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
