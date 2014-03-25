using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ContactsApp.CustomControls
{
    public class DynamicContentControl : ContentControl
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            this.ContentTemplate = newContent == null ? null : DataTemplateSelector.GetTemplate(newContent.GetType().ToString());
        }
    }
}
