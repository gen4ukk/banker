using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmsSynchronizer.Model
{
    public class NavigationMenuItemModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Page PageObj { get; set; }

        public Type PageType { get; set; }

        public ImageSource IconSource { get; set; }
    }
}
