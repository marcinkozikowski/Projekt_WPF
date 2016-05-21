using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Projekt_WPF_Solution.Validators
{
    public static class Validator
    {

        public static bool IsValid(DependencyObject parent)
        {
            bool valid = true;

            if (Validation.GetHasError(parent))
            {
                valid = false;
            }
            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child))
                {
                    valid = false;
                }
            }

            return valid;
        }

    }
}
