using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmsSynchronizer.View
{

    public class NavigationMenuItem
    {
        public NavigationMenuItem()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Page PageObj { get; set; }
        public ImageSource IconSource { get; set; }
    }
}