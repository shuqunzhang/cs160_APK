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
    /// Interaction logic for TabooWords.xaml
    /// </summary>
    public partial class TabooWords : Page
    {
        public TabooWords()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (String g in MainWindow.tabooWords)
            {
                wordsListBox.Items.Add(g);
            }

            List<Button> activeButtons = new List<Button>();
            foreach (UIElement element in cWords.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button)element);
                }
            }

            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.gestures);
            MainWindow.setCurrentPage(this);
            okButton.Visibility = System.Windows.Visibility.Hidden;
            cancelButton.Visibility = System.Windows.Visibility.Hidden;
            okButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            nameInputPanel.Visibility = System.Windows.Visibility.Visible;
            panelBorder.Visibility = System.Windows.Visibility.Visible;
            okButton.Visibility = System.Windows.Visibility.Visible;
            okButton.IsEnabled = true;
            cancelButton.Visibility = System.Windows.Visibility.Visible;
            cancelButton.IsEnabled = true;
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            if (wordsListBox.SelectedItem != null)
            {
                MainWindow.tabooWords.Remove(wordsListBox.SelectedItem.ToString());
                wordsListBox.Items.Remove(wordsListBox.SelectedItem);
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SettingsPage());
        }

        private void okClick(object sender, RoutedEventArgs e)
        {
            if (nameInput.Text == "" || MainWindow.tabooWords.Contains(nameInput.Text))
            {
                nameInputLabel.Content = "Invalid Word, try again.";
                return;
            }
            MainWindow.tabooWords.Add(nameInput.Text);
            wordsListBox.Items.Add(nameInput.Text);
            nameInput.Text = ""; //clears the text box
            nameInputLabel.Content = "Say a taboo word.";
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
            nameInputLabel.Content = "Say a taboo word.";
            nameInputPanel.Visibility = System.Windows.Visibility.Hidden;
            panelBorder.Visibility = System.Windows.Visibility.Hidden;
            okButton.Visibility = System.Windows.Visibility.Hidden;
            okButton.IsEnabled = false;
            cancelButton.Visibility = System.Windows.Visibility.Hidden;
            cancelButton.IsEnabled = false;
        }

    }
}