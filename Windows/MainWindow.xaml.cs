using Microsoft.Win32;
using ModManager.Handler;
using ModManager.Windows;
using ModManager;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;


namespace ModManager
{
    public partial class MainWindow : Window 
    {
        public MainWindow() 
        {
            InitializeComponent();
            Mod.ModsList = new ObservableCollection<Mod>(CacheHandler.LoadMods());
            ModsDataGrid.ItemsSource = Mod.ModsList;
        }

        private void AddModsButton_Click(object sender, RoutedEventArgs e) 
        {
            ModImportWindow modImportWindow = new ModImportWindow();
            modImportWindow.Show();
        }

        private void RemoveModsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMods = ModsDataGrid.SelectedItems.Cast<Mod>().ToList();
            foreach (var mod in selectedMods)
            { 
                Mod.ModsList.Remove(mod);
            }
            CacheHandler.DeleteMods(selectedMods);
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

        private void Enabled_Checked(object sender, RoutedEventArgs e)
        {
            // Add Logic
        }

        private void Enabled_Unchecked(object sender, RoutedEventArgs e)
        {
            // Add Logic
        }
    }
}
