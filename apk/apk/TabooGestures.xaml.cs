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
    /// Interaction logic for TabooGestures.xaml
    /// </summary>
    public partial class TabooGestures : Page
    {
        public TabooGestures()
        {
            InitializeComponent();
        }

        //public TabooGestures(KinectSensorChooser ksc) 
        //{
        //    InitializeComponent();
        //    kinectSensorChooser1 = ksc;
        //}



        //Calling this method changes the visibility of the new gesture notification window between
        //visible and hidden, depending on its current state
        private void change_notification_visibility()
        {
            if (NotificationCanvas.Visibility == System.Windows.Visibility.Hidden)
            {
                NotificationCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                NotificationCanvas.Visibility = Visibility.Hidden;
            }
        }

        //Calling this method changes the visibility of the gesture name notification window between
        //visible and hidden, depending on its current state
        private void change_nameNotification_visibility()
        {
            if (GestureNameNotification.Visibility == System.Windows.Visibility.Hidden)
            {
                GestureNameNotification.Visibility = Visibility.Visible;
            }
            else
            {
                GestureNameNotification.Visibility = Visibility.Hidden;
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (GestureNameInput == null)
            {
                return;
            }
            gestureListBox.Items.Add(GestureNameInput.Text);
            GestureNameInput.Text = ""; //clears the text box
            change_nameNotification_visibility(); 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gestureListBox.Items.Add("*Add New Gesture*");
            
            //all this ugliness is to do a deep copy of all buttons on this page
            List<Button> activeButtons = new List<Button>();
            //so as to not call cGestures.children again
            UIElementCollection outer = cGestures.Children; 
            UIElement[] outerCpy = new UIElement[outer.Count];
            List<UIElement> inner = new List<UIElement>();
            outer.CopyTo(outerCpy, 0);

            while (inner != null)
            {
                foreach (UIElement element in outerCpy)
                {
                    if (element is Button)
                    {
                        activeButtons.Add((Button)element);
                    }
                    else 
                    {
                        if (element is Canvas) //assumed all buttons would be in a canvas
                        {
                            foreach (UIElement ele in ((Canvas)element).Children)
                            {
                                inner.Add(ele);
                            }
                        }
                    }
                }

                //copied all the buttons in a layer, now go deeper
                if (inner.Count > 0)
                {
                    outerCpy = inner.ToArray();
                    inner.Clear();
                }
                else 
                {
                    inner = null;
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.settings);
            MainWindow.setCurrentPage(this);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SettingsPage());
        }
    }
}
