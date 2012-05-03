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
            foreach (String g in MainWindow.tabooGestures.Keys){
                gestureListBox.Items.Add(g);
            }

            List<Button> activeButtons = new List<Button>();
            foreach (UIElement element in cGestures.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button)element);
                }
            }

            ////all this ugliness is to do a deep copy of all buttons on this page
            //List<Button> activeButtons = new List<Button>();
            ////so as to not call cGestures.children again
            //UIElementCollection outer = cGestures.Children; 
            //UIElement[] outerCpy = new UIElement[outer.Count];
            //List<UIElement> inner = new List<UIElement>();
            //outer.CopyTo(outerCpy, 0);

            //while (inner != null)
            //{
            //    foreach (UIElement element in outerCpy)
            //    {
            //        if (element is Button)
            //        {
            //            activeButtons.Add((Button)element);
            //        }
            //        else 
            //        {
            //            if (element is Canvas) //assumed all buttons would be in a canvas
            //            {
            //                foreach (UIElement ele in ((Canvas)element).Children)
            //                {
            //                    inner.Add(ele);
            //                }
            //            }
            //        }
            //    }

            //    //copied all the buttons in a layer, now go deeper
            //    if (inner.Count > 0)
            //    {
            //        outerCpy = inner.ToArray();
            //        inner.Clear();
            //    }
            //    else 
            //    {
            //        inner = null;
            //    }
            //}
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.gestures);
            MainWindow.setCurrentPage(this);
            okButton.Visibility = System.Windows.Visibility.Hidden;
            okButton.IsEnabled = false;
            cancelButton.Visibility = System.Windows.Visibility.Hidden;
            cancelButton.IsEnabled = false;
        }

        public void goNameInput()
        {
            directionsPanel.Visibility = System.Windows.Visibility.Hidden;
            nameInputPanel.Visibility = System.Windows.Visibility.Visible;
            okButton.Visibility = System.Windows.Visibility.Visible;
            okButton.IsEnabled = true;
            cancelButton.Visibility = System.Windows.Visibility.Visible;
            cancelButton.IsEnabled = true;
        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            directionsPanel.Visibility = System.Windows.Visibility.Visible;
            panelBorder.Visibility = System.Windows.Visibility.Visible;
            MainWindow.setTempPos = true;
            cGestures.Focus();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            if (gestureListBox.SelectedItem != null)
            {
                MainWindow.tabooGestures.Remove(gestureListBox.SelectedItem.ToString());
                gestureListBox.Items.Remove(gestureListBox.SelectedItem);
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SettingsPage());
        }

        private void okClick(object sender, RoutedEventArgs e)
        {
            if (nameInput.Text == "" || MainWindow.tabooGestures.ContainsKey(nameInput.Text))
            {
                nameInputLabel.Content = "Invalid Gesture Name, try again.";
                return;
            }
            MainWindow.addTabooGesture(nameInput.Text);
            gestureListBox.Items.Add(nameInput.Text);
            nameInput.Text = ""; //clears the text box
            nameInputLabel.Content = "Say a name for this gesture.";
            nameInputPanel.Visibility = System.Windows.Visibility.Hidden;
            panelBorder.Visibility = System.Windows.Visibility.Hidden;
            okButton.Visibility = System.Windows.Visibility.Hidden;
            okButton.IsEnabled = false;
            cancelButton.Visibility = System.Windows.Visibility.Hidden;
            cancelButton.IsEnabled = false;
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            nameInput.Text = ""; //clears the text box
            nameInputLabel.Content = "Say a name for this gesture.";
            nameInputPanel.Visibility = System.Windows.Visibility.Hidden;
            panelBorder.Visibility = System.Windows.Visibility.Hidden;
            okButton.Visibility = System.Windows.Visibility.Hidden;
            okButton.IsEnabled = false;
            cancelButton.Visibility = System.Windows.Visibility.Hidden;
            cancelButton.IsEnabled = false;
        }

        void proceed(object sender, KeyEventArgs e) //aka wizard of oz
        {
            switch (e.Key)
            {
                case Key.Return:
                    if (directionsPanel.Visibility == System.Windows.Visibility.Visible)
                    {
                        MainWindow.setTempPos = true;
                        goNameInput();
                    }
                    break;
            }
        }

    }
}
