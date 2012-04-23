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

        private Result currentResult;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            currentResult = new Result();
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
            currentResult.duration = 0;
        }
        public void changeTime(TimeSpan t) //t = now - loadTime - 5 secs
        {
            timeLabel.Content = "" + t.Minutes + ":" + (t.Seconds < 10 ? "0" + t.Seconds : "" + t.Seconds);
        }

        public void finish()
        {
            currentResult.time = (String)timeLabel.Content;
            this.NavigationService.Navigate(new ResultPage(currentResult));
        }

        public void tick()
        {
            currentResult.duration++;
        }
        public void clearHilights()
        {
            postureLabel.Foreground = Brushes.Gray;
            postureLabel.Background = Brushes.Gainsboro;
            motionLabel.Foreground = Brushes.Gray;
            motionLabel.Background = Brushes.White;
            volumeLabel.Foreground = Brushes.Gray;
            volumeLabel.Background = Brushes.Gainsboro;
            gestureLabel.Foreground = Brushes.Gray;
            gestureLabel.Background = Brushes.Gainsboro;
            gestureDetail.Foreground = Brushes.White;
            wordLabel.Foreground = Brushes.Gray;
            wordLabel.Background = Brushes.Gainsboro;
            wordLabel.Foreground = Brushes.White;
        }
        public void logPosture()
        {
            postureLabel.Foreground = Brushes.Red;
            postureLabel.Background = Brushes.Black;
            currentResult.posture++;
        }
        public void logRange()
        {
            motionLabel.Foreground = Brushes.Red;
            motionLabel.Background = Brushes.Black;
            currentResult.motionRange++;
        }
        public void logVolume()
        {
            volumeLabel.Foreground = Brushes.Red;
            volumeLabel.Background = Brushes.Black;
            currentResult.volume++;
        }
        public void logGesture(String g)
        {
            gestureLabel.Foreground = Brushes.Red;
            gestureLabel.Background = Brushes.Black;
            gestureDetail.Content = g;
            gestureDetail.Foreground = Brushes.Black;
            currentResult.totalGestures++;
            if (currentResult.gestures.ContainsKey(g))
            {
                currentResult.gestures[g]++;
            }
            else
            {
                currentResult.gestures.Add(g, 1);
            }
        }
        public void logWord(String w)
        {
            wordLabel.Foreground = Brushes.Red;
            wordLabel.Background = Brushes.Black;
            wordDetail.Content = w;
            wordDetail.Foreground = Brushes.Black;
            MainWindow.lastNoteTime = DateTime.Now;
            currentResult.totalWords++;
            if (currentResult.words.ContainsKey(w))
            {
                currentResult.words[w]++;
            }
            else
            {
                currentResult.words.Add(w, 1);
            }
        }

        void onLabels(object sender, KeyEventArgs e) //aka wizard of oz
        {
            switch (e.Key)
            {
                case Key.P:
                    postureLabel.Foreground = Brushes.Red;
                    postureLabel.Background = Brushes.Black;
                    break;
                case Key.M:
                    motionLabel.Foreground = Brushes.Red;
                    motionLabel.Background = Brushes.Black;
                    break;
                case Key.G:
                    gestureLabel.Foreground = Brushes.Red;
                    gestureLabel.Background = Brushes.Black;
                    gestureDetail.Foreground = Brushes.Black;
                    break;
                case Key.W:
                    wordLabel.Foreground = Brushes.Red;
                    wordLabel.Background = Brushes.Black;
                    wordDetail.Foreground = Brushes.Black;
                    break;
                case Key.V:
                    volumeLabel.Foreground = Brushes.Red;
                    volumeLabel.Background = Brushes.Black;
                    break;
                case Key.NumPad1:
                    if (MainWindow.tabooGestures.Count > 0)
                    {
                        gestureDetail.Content = MainWindow.tabooGestures.ElementAt(0).Key;
                        gestureLabel.Foreground = Brushes.Red;
                        gestureLabel.Background = Brushes.Black;
                        gestureDetail.Foreground = Brushes.Black;
                    }
                    break;
                case Key.NumPad2:
                    if (MainWindow.tabooGestures.Count > 1)
                    {
                        gestureDetail.Content = MainWindow.tabooGestures.ElementAt(1).Key;
                        gestureLabel.Foreground = Brushes.Red;
                        gestureLabel.Background = Brushes.Black;
                        gestureDetail.Foreground = Brushes.Black;
                    }
                    break;
                case Key.NumPad3:
                    if (MainWindow.tabooGestures.Count > 2)
                    {
                        gestureDetail.Content = MainWindow.tabooGestures.ElementAt(2).Key;
                        gestureLabel.Foreground = Brushes.Red;
                        gestureLabel.Background = Brushes.Black;
                        gestureDetail.Foreground = Brushes.Black;
                    }
                    break;
                case Key.NumPad4:
                    if (MainWindow.tabooWords.Count > 0)
                    {
                        logWord(MainWindow.tabooWords.ElementAt(0));
                        wordDetail.Content = MainWindow.tabooWords.ElementAt(0);
                        wordLabel.Foreground = Brushes.Red;
                        wordLabel.Background = Brushes.Black;
                        wordDetail.Foreground = Brushes.Black;
                    }
                    break;
                case Key.NumPad5:
                    if (MainWindow.tabooWords.Count > 1)
                    {
                        logWord(MainWindow.tabooWords.ElementAt(1));
                        wordDetail.Content = MainWindow.tabooWords.ElementAt(1);
                        wordLabel.Foreground = Brushes.Red;
                        wordLabel.Background = Brushes.Black;
                        wordDetail.Foreground = Brushes.Black;
                    }
                    break;
                case Key.NumPad6:
                    if (MainWindow.tabooWords.Count > 2)
                    {
                        logWord(MainWindow.tabooWords.ElementAt(2));
                        wordDetail.Content = MainWindow.tabooWords.ElementAt(2);
                        wordLabel.Foreground = Brushes.Red;
                        wordLabel.Background = Brushes.Black;
                        wordDetail.Foreground = Brushes.Black;
                    }
                    break;
                case Key.Escape:
                    finish();
                    break;
            }
        }


        void offLabels(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.P:
                    postureLabel.Foreground = Brushes.Gray;
            postureLabel.Background = Brushes.Gainsboro;
                    break;
                case Key.M:
                    motionLabel.Foreground = Brushes.Gray;
            motionLabel.Background = Brushes.White;
                    break;
                case Key.G:
                    gestureLabel.Foreground = Brushes.Gray;
                    gestureDetail.Foreground = Brushes.White;
            gestureLabel.Background = Brushes.Gainsboro;
                    break;
                case Key.W:
                    wordLabel.Foreground = Brushes.Gray;
                    wordDetail.Foreground = Brushes.White;
            wordLabel.Background = Brushes.Gainsboro;
                    break;
                case Key.V:
                    volumeLabel.Foreground = Brushes.Gray;
            volumeLabel.Background = Brushes.Gainsboro;
                    break;
                case Key.NumPad1:
                    if (MainWindow.tabooGestures.Count > 0)
                    {
                        gestureLabel.Foreground = Brushes.Gray;
                        gestureLabel.Background = Brushes.Gainsboro;
                        gestureDetail.Foreground = Brushes.White;
                    }
                    break;
                case Key.NumPad2:
                    if (MainWindow.tabooGestures.Count > 1)
                    {
                        gestureLabel.Foreground = Brushes.Gray;
                        gestureLabel.Background = Brushes.Gainsboro;
                        gestureDetail.Foreground = Brushes.White;
                    }
                    break;
                case Key.NumPad3:
                    if (MainWindow.tabooGestures.Count > 2)
                    {
                        gestureLabel.Foreground = Brushes.Gray;
                        gestureLabel.Background = Brushes.Gainsboro;
                        gestureDetail.Foreground = Brushes.White;
                    }
                    break;
                case Key.NumPad4:
                    if (MainWindow.tabooWords.Count > 0)
                    {
                        wordLabel.Foreground = Brushes.Gray;
                        wordLabel.Background = Brushes.Gainsboro;
                        wordDetail.Foreground = Brushes.White;
                    }
                    break;
                case Key.NumPad5:
                    if (MainWindow.tabooWords.Count > 1)
                    {
                        wordLabel.Foreground = Brushes.Gray;
                        wordLabel.Background = Brushes.Gainsboro;
                        wordDetail.Foreground = Brushes.White;
                    }
                    break;
                case Key.NumPad6:
                    if (MainWindow.tabooWords.Count > 2)
                    {
                        wordLabel.Foreground = Brushes.Gray;
                        wordLabel.Background = Brushes.Gainsboro;
                        wordDetail.Foreground = Brushes.White;
                    }
                    break;
            }
        }
    }
}
