﻿using System;
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

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for AddNewCarWindow.xaml
    /// </summary>
    public partial class AddNewCarWindow : Window
    {
        public AddNewCarWindow()
        {
            InitializeComponent();
            MarkaComboBox.Items.Add("Audi");
        }
    }
}
