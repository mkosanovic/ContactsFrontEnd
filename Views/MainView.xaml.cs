using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ContactsApp.ViewModels;

namespace ContactsApp.Views
{
    public partial class MainView : PhoneApplicationPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            
        }

        private void Help_Click(object sender, EventArgs e)
        {
            (this.DataContext as BaseViewModel).Help();
        }

        private void RandomData_Click(object sender, EventArgs e)
        {

        }

        private void NewContact_Click(object sender, EventArgs e)
        {
            (this.DataContext as BaseViewModel).NewContact();
        }
    }
}