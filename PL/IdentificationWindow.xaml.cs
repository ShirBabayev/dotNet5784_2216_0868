using PL.Engineer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace PL
{
    /// <summary>
    /// Interaction logic for IdentificationWindow.xaml
    /// </summary>
    public partial class IdentificationWindow : Window, INotifyPropertyChanged
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IdentificationWindow()
        {
            InitializeComponent();
            DataContext = this; // Set the DataContext to the current instance of IdentificationWindow

        }

        private void Confirmation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.Engineer.Read((Convert.ToInt32(Id))) != null)
                {
                    new EngineerDetailesWindow(Convert.ToInt32(Id)).Show();
                }
            }
            catch (Exception ex)
            {
                if (Id != "")
                {
                    MessageBox.Show($"There is no Engineer with the id: {Id}", "Engineer Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("No Id was pressed, try again", "No Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    Confirmation_Click(sender, e);
            //}
        }


        private string _id = "";

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}