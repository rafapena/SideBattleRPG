using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Database.Classes;
using Database.Utilities;
using System.Windows.Media;

namespace Database.BaseControls
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {   
        public Footer()
        {
            InitializeComponent();
        }

        // Sets the visibility of the buttons when the user selects 'New'
        public void ApplyInitializeNewSettings()
        {
            DeleteButton.Visibility = Visibility.Collapsed;
            CloneButton.Visibility = Visibility.Collapsed;
            CreateButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Collapsed;
        }

        // Sets the visibility of the buttons when reading the table
        public void ApplyReadSettings()
        {
            DeleteButton.Visibility = Visibility.Visible;
            CloneButton.Visibility = Visibility.Visible;
            CreateButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Visible;
        }

        // Changes the text at the bottom of the buttons
        public void SetOperationMessage(string footerMsg, Color color)
        {
            FooterMessage.Text = footerMsg;
            FooterMessage.Foreground = new SolidColorBrush(color);
        }

        // Removes the text at the bottom of the buttons
        public void RemoveOperationMessage()
        {
            FooterMessage.Text = "";
        }

        // Trigger page functions
        private void Deleted(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Delete();
        }
        private void Cloned(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Clone();
        }
        private void Created(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Create();
        }
        private void Updated(object sender, EventArgs e)
        {
            (Application.Current.MainWindow.Content as _ClassOperations).Update();
        }
    }
}