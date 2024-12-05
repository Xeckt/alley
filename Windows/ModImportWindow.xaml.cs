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
using System.Windows.Shapes;
using Microsoft.Win32;
using ModManager.Handler;
using SharpCompress.Common;

namespace ModManager.Windows
{
    /// <summary>
    /// Interaction logic for ModImportWindow.xaml
    /// </summary>
    public partial class ModImportWindow : Window
    {

        private static string SelectedType { get; set;  } = "";

        public ModImportWindow()
        {
            InitializeComponent();
        }

        public static ModType GetSelectedType() {
            switch (SelectedType)
            {
                case "Map":
                    return ModType.Map;
                case "Clothing":
                    return ModType.Clothing;
                case "Animation":
                    return ModType.Animation;
                case "Utility":
                    return ModType.Utility;
                case "Blueprint":
                    return ModType.Blueprint;
                case "Custom":
                    return ModType.Custom;
            }
            return ModType.Unknown;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var mod in ModsList.Items) { 
                ArchiveHandler.ExtractAndImportMod(mod.ToString());
            }
            this.Close();
        }

        private void SelectModsButton_Click(object sender, RoutedEventArgs e)
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
                        ModsList.Items.Add(filePath);
                    } 
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to add mod to list: {ex.Message}");
                    }
                }
            }
        }

        private void RemoveSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = ModsList.SelectedItems.Cast<object>().ToList();

            if (ModsList.SelectedItems.Count > 0 && ModsList.SelectedItems != null) 
            {
                foreach (var mod in selectedItems)
                {
                    ModsList.Items.Remove(mod);
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selectedCheckbox = sender as CheckBox;

            foreach (var child in ((StackPanel)selectedCheckbox.Parent).Children)
            {
                if (child is CheckBox checkbox && checkbox != selectedCheckbox)
                {
                    checkbox.IsEnabled = false;
                    SelectedType = selectedCheckbox.Name;
                }
            }

            if (ModsList.Items.Count > 0) // TODO: Fix this because if you select the button first then import, it doesn't enable the import button.
            {
                ImportModsButton.IsEnabled = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var child in ((StackPanel)((CheckBox)sender).Parent).Children)
            {
                if (child is CheckBox checkbox)
                {
                    checkbox.IsEnabled = true;
                }
            }
            ImportModsButton.IsEnabled = false;
        }

    }
}
