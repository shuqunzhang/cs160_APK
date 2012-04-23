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
        Result currentResult;

        public ResultPage()
        {
            currentResult = new Result();
            currentResult.motionRange = 0;
            currentResult.volume = 0;
            currentResult.posture = 0;
            currentResult.totalWords = 2;
            currentResult.totalGestures = 3;
            currentResult.gestures.Add("Hands in Pockets", 2);
            currentResult.gestures.Add("Crossed Arms", 1);
            currentResult.words.Add("Um", 2);
            InitializeComponent();
            timer.Tick += new EventHandler(hideSaved);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        public ResultPage(Result r)
        {
            InitializeComponent();
            currentResult = r;
            timer.Tick += new EventHandler(hideSaved);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private String formatReversePercent(double num, double denom)
        {
            return (int)(100 - (num / denom) * 100) + "%";
        }
        private String formatPercent(double num, double denom)
        {

            return (int)((num / denom) * 100) + "%";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            time.Content = currentResult.time;
            rangeCheck.Content = formatReversePercent(currentResult.motionRange,currentResult.duration);
            volumeCheck.Content = formatReversePercent(currentResult.volume, currentResult.duration);
            postureCheck.Content = formatReversePercent(currentResult.posture, currentResult.duration);
            gesturesCount.Content = formatPercent(currentResult.totalGestures,currentResult.duration);
            wordsCount.Content = currentResult.totalWords;

            
            foreach (String g in currentResult.gestures.Keys){
                gesturesListBox.Items.Add(g + " : " + formatPercent(currentResult.gestures[g],currentResult.duration));
            }

            foreach (String w in currentResult.words.Keys)
            {
                wordsListBox.Items.Add(w + " : " + currentResult.words[w]);
            }

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
            currentResult.name = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            MainWindow.results.Add(currentResult.name, currentResult);
            savedLabel.Visibility = System.Windows.Visibility.Visible;
            MainWindow.lastNoteTime = DateTime.Now;
            saveButton.IsEnabled = false;
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