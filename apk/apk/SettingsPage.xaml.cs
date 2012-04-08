using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Samples.Kinect.WpfViewers;
using Coding4Fun.Kinect.Wpf;
using System.Windows.Threading;

namespace apk
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //kinectSensorChooser1.Kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            List<Button> activeButtons = new List<Button>();
            foreach (UIElement element in cSettings.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button)element);
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.settings);
            MainWindow.setCurrentPage(this);
        }

        private void rangeClick(object sender, RoutedEventArgs e)
        {
        }

        private void gesturesClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TabooGestures());
        }

        private void wordsClick(object sender, RoutedEventArgs e)
        {
        }

        private void homeClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void volumeClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
