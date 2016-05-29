using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt_WPF_Solution.Commands
{
    public class MyCommands
    {
        private static RoutedUICommand delete;
        private static RoutedUICommand edit;

        static MyCommands()
        {
            delete = new RoutedUICommand("Delete", "del", typeof(MyCommands));
            edit = new RoutedUICommand("Edit", "edit", typeof(MyCommands));
        }

        public static RoutedUICommand Delete
        {
            get { return delete; }
            set { delete = value; }
        }

        public static RoutedUICommand Edit
        {
            get { return edit; }
            set { edit = value; }
        }
    }
}
