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
        
        DispatcherTimer timer = new DispatcherTimer();

        public SettingsPage()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(hideSaved);
            timer.Interval = new TimeSpan(0, 0, 1);
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
            cSettings.Focus();
            rightRangePanel.Visibility = System.Windows.Visibility.Visible;
            panelBorder.Visibility = System.Windows.Visibility.Visible;
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
            cSettings.Focus();
            volumePanel.Visibility = System.Windows.Visibility.Visible;
            panelBorder.Visibility = System.Windows.Visibility.Visible;
        }

        void proceed(object sender, KeyEventArgs e) //aka wizard of oz
        {
            switch (e.Key){
                case Key.Return:
                    if(volumePanel.Visibility == System.Windows.Visibility.Visible){
                        volumePanel.Visibility = System.Windows.Visibility.Hidden;
                        panelBorder.Visibility = System.Windows.Visibility.Hidden;
                        savedLabel.Visibility = System.Windows.Visibility.Visible;
                        MainWindow.lastNoteTime = DateTime.Now;
                        timer.Start();
                    } else if (rightRangePanel.Visibility == System.Windows.Visibility.Visible){
                        rightRangePanel.Visibility = System.Windows.Visibility.Hidden;
                        leftRangePanel.Visibility = System.Windows.Visibility.Visible;
                    } else if (leftRangePanel.Visibility == System.Windows.Visibility.Visible){
                        leftRangePanel.Visibility = System.Windows.Visibility.Hidden;
                        panelBorder.Visibility = System.Windows.Visibility.Hidden;
                        savedLabel.Visibility = System.Windows.Visibility.Visible;
                        MainWindow.lastNoteTime = DateTime.Now;
                        timer.Start();
                    }
                    break;
            }
        }
        private void hideSaved(object sender, EventArgs e)
        {
            timer.Stop();
            savedLabel.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
