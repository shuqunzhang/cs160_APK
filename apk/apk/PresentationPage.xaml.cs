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
            cPresentation.Focus();
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
        public void changeTime(int i) //i = now - loadTime - 5
        {
            timeLabel.Content = "" + i / 60 + ":" + (i % 60 < 10 ? "0"+i % 60 : ""+i % 60);
        }

        void toggleLabels(object sender, KeyEventArgs e) //aka wizard of oz
        {
            switch (e.Key)
            {
                case Key.P:
                    postureLabel.Foreground = (postureLabel.Foreground == Brushes.Gray) ? Brushes.Red : Brushes.Gray;
                    break;
                case Key.M:
                    motionLabel.Foreground = (motionLabel.Foreground == Brushes.Gray) ? Brushes.Red : Brushes.Gray;
                    break;
                case Key.G:
                    gestureLabel.Foreground = (gestureLabel.Foreground == Brushes.Gray) ? Brushes.Red : Brushes.Gray;
                    gestureDetail.Foreground = (gestureDetail.Foreground == Brushes.White) ? Brushes.Black : Brushes.White;
                    break;
                case Key.W:
                    wordLabel.Foreground = (wordLabel.Foreground == Brushes.Gray) ? Brushes.Red : Brushes.Gray;
                    wordDetail.Foreground = (wordDetail.Foreground == Brushes.White) ? Brushes.Black : Brushes.White;
                    break;
                case Key.V:
                    volumeLabel.Foreground = (volumeLabel.Foreground == Brushes.Gray) ? Brushes.Red : Brushes.Gray;
                    break;
            }
        }
    }
}
