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

namespace apk
{
    /// <summary>
    /// Interaction logic for ReviewPage.xaml
    /// </summary>
    public partial class ReviewPage : Page
    {
        public ReviewPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (String pres in MainWindow.results.Keys)
            {
                resultsListBox.Items.Add(pres);
            }

            List<Button> activeButtons = new List<Button>();
            foreach (UIElement element in cReview.Children)
            {
                if (element is Button)
                {
                    activeButtons.Add((Button)element);
                }
            }
            MainWindow.updateButtons(activeButtons);
            MainWindow.setCurrentPageType(MainWindow.PageType.review);
            MainWindow.setCurrentPage(this);
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        private void resultClick(object sender, RoutedEventArgs e)
        {
            if (resultsListBox.SelectedItem != null)
            {
                this.NavigationService.Navigate(new ReviewResultPage(MainWindow.results[resultsListBox.SelectedItem.ToString()]));
            }
        }
    }
}
