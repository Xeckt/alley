﻿using Microsoft.Win32;
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
            ModsDataGrid.ItemsSource = Mods;
        }

        private void AddModsButton_Click(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog {
                Filter = "Compressed Files (*.zip;*.rar;*.7z)|*.zip;*.rar;*.7z",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true) {
                foreach (var filePath in openFileDialog.FileNames) {
                    try {
                        var mod = ArchiveHandler.ExtractMod(filePath);
                        Mods.Add(mod);
                    } catch (Exception ex) {
                        MessageBox.Show($"Failed to add mod: {ex.Message}");
                    }
                }
            }
        }

        private void RemoveModsButton_Click(object sender, RoutedEventArgs e) {
            var selectedMods = ModsDataGrid.SelectedItems;
            for (var i = Mods.Count - 1; i >= 0; i--) { 
                var mod = Mods[i];
                if (selectedMods.Contains(mod)) {
                    Mods.Remove(mod);
                }
            }
        }

        private void ExportProfileButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Export Profile feature is not implemented yet.");
        }
    }
}
