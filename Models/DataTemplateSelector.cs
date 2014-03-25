using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactsApp.Models
{
    public static class DataTemplateSelector
    {
        public static DataTemplate GetTemplate(string key)
        {
            return App.Current.Resources[key] as DataTemplate;
        }
    }
}
