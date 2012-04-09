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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //gestureListBox.Items.Add("*Add New Gesture*");
            
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
            MainWindow.setCurrentPageType(MainWindow.PageType.gestures);
            MainWindow.setCurrentPage(this);
        }

        public void goNameInput()
        {
            directionsPanel.Visibility = System.Windows.Visibility.Hidden;
            nameInputPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            directionsPanel.Visibility = System.Windows.Visibility.Visible;
            panelBorder.Visibility = System.Windows.Visibility.Visible;
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            if (gestureListBox.SelectedItem != null)
            {
                gestureListBox.Items.Remove(gestureListBox.SelectedItem);
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SettingsPage());
        }

        private void okClick(object sender, RoutedEventArgs e)
        {
            if (nameInput == null)
            {
                return;
            }
            gestureListBox.Items.Add(nameInput.Text);
            nameInput.Text = ""; //clears the text box
            nameInputPanel.Visibility = System.Windows.Visibility.Hidden;
            panelBorder.Visibility = System.Windows.Visibility.Hidden;
        }

    }
}
