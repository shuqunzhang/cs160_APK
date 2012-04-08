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
    /// Interaction logic for PresentationPage.xaml
    /// </summary>
    public partial class PresentationPage : Page
    {
        public PresentationPage()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            startPanel.Visibility = System.Windows.Visibility.Visible;
            //kinectSensorChooser1.Kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            List<Button> activeButtons = new List<Button>();
            foreach (UIElement element in cPresentation.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button)element);
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.loadedTime = DateTime.Now;
            MainWindow.setCurrentPageType(MainWindow.PageType.presentation);
            MainWindow.setCurrentPage(this);
        }

        public void changeCount(int i){
            startTime.Content = i;
        }
        public void finishCount(){
            startPanel.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
