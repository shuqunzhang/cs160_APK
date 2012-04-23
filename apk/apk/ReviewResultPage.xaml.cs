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
    public partial class ReviewResultPage : Page
    {

        Result currentResult;

        public ReviewResultPage(Result r)
        {
            currentResult = r;
            InitializeComponent();
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
            rangeCheck.Content = formatReversePercent(currentResult.motionRange, currentResult.duration);
            volumeCheck.Content = formatReversePercent(currentResult.volume, currentResult.duration);
            postureCheck.Content = formatReversePercent(currentResult.posture, currentResult.duration);
            gesturesCount.Content = formatPercent(currentResult.totalGestures, currentResult.duration);
            wordsCount.Content = currentResult.totalWords;


            foreach (String g in currentResult.gestures.Keys)
            {
                gesturesListBox.Items.Add(g + " : " + formatPercent(currentResult.gestures[g], currentResult.duration));
            }

            foreach (String w in currentResult.words.Keys)
            {
                wordsListBox.Items.Add(w + " : " + currentResult.words[w]);
            }

            List<Button> activeButtons = new List<Button>();
            foreach (UIElement element in cRR.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button)element);
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.result);
            MainWindow.setCurrentPage(this);
            yesButton.Visibility = System.Windows.Visibility.Hidden;
            yesButton.IsEnabled = false;
            noButton.Visibility = System.Windows.Visibility.Hidden;
            noButton.IsEnabled = false;
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ReviewPage());
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            confirmationLabel.Visibility = System.Windows.Visibility.Visible;
            yesButton.Visibility = System.Windows.Visibility.Visible;
            yesButton.IsEnabled = true;
            noButton.Visibility = System.Windows.Visibility.Visible;
            noButton.IsEnabled = true;
        }

        private void yesClick(object sender, RoutedEventArgs e)
        {
            MainWindow.results.Remove(currentResult.name);
            this.NavigationService.Navigate(new ReviewPage());
        }

        private void noClick(object sender, RoutedEventArgs e)
        {
            confirmationLabel.Visibility = System.Windows.Visibility.Hidden;
            yesButton.Visibility = System.Windows.Visibility.Hidden;
            yesButton.IsEnabled = false;
            noButton.Visibility = System.Windows.Visibility.Hidden;
            noButton.IsEnabled = false;
        }

        private void rewatchClick(object sender, RoutedEventArgs e)
        {
        }
    }
}