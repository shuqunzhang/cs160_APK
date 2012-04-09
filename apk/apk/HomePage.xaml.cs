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
using Coding4Fun.Kinect.Wpf;
using Microsoft.Samples.Kinect.WpfViewers;
using System.Windows.Threading;

namespace apk
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Button> activeButtons = new List<Button>();
            foreach(UIElement element in cHomePage.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button) element);
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.home);
            MainWindow.setCurrentPage(this);
        }

        private void beginClick(object sender, RoutedEventArgs e)
        {
            PresentationPage ppage = new PresentationPage();
            this.NavigationService.Navigate(ppage);
        }

        private void reviewClick(object sender, RoutedEventArgs e)
        {
            //ResultPage rpage = new ResultPage();
            //this.NavigationService.Navigate(rpage);
        }

        private void settingsClick(object sender, RoutedEventArgs e)
        {
            SettingsPage spage = new SettingsPage();
            this.NavigationService.Navigate(spage);
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            MainWindow.closeApplication();
        }

    }
}
