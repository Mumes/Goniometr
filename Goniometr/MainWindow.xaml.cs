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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO.Ports;
using System.Resources;

namespace Goniometr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel gcc;
        public MainWindow()
        {
            InitializeComponent();
            gcc = new MainWindowViewModel();
            DataContext = gcc;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();
            //playing with camera device
            TIS.Imaging.ICImagingControl ic = new TIS.Imaging.ICImagingControl();
            gcc.ICUsbCamControl = ic;
            
            ic.Width = 250;
            ic.Height = 250;
            ic.BackColor = System.Drawing.Color.Red;
            
            ic.BackgroundImage =Goniometr.Properties.Resources.nocam;
            //ic.ShowDeviceSettingsDialog();
            //ic.LiveStart();
            host.Child = ic;
            this.icgrid.Children.Add(host);
        }
    }
}
