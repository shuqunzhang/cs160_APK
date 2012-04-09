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
    /// Interaction logic for ReviewPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {

        DispatcherTimer timer = new DispatcherTimer();

        public ResultPage()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(hideSaved);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gesturesListBox.Items.Add("Taboo Gestures Breakdown:");
            wordsListBox.Items.Add("Taboo Words Breakdown:");

            List<Button> activeButtons = new List<Button>();
            foreach(UIElement element in cResult.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button) element);
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.result);
            MainWindow.setCurrentPage(this);
        }

        private void returnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            savedLabel.Visibility = System.Windows.Visibility.Visible;
            MainWindow.lastNoteTime = DateTime.Now;
            timer.Start();
        }

        private void presentClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PresentationPage());
        }

        private void hideSaved(object sender, EventArgs e)
        {
            timer.Stop();
            savedLabel.Visibility = System.Windows.Visibility.Hidden;
        }

    }
}