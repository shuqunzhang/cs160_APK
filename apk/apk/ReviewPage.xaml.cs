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
    public partial class ReviewPage : Page
    {
        public ReviewPage()
        {
            InitializeComponent();
        }

        public ReviewPage(KinectSensorChooser ks) 
        {
            InitializeComponent();
            kinectSensorChooser1 = ks;
        }

        #region vars
        bool closing = false;
        const int skeletonCount = 6;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];

        private Dictionary<JointType, SkeletonPoint> currentPos = new Dictionary<JointType, SkeletonPoint>() {
            {JointType.HandRight, new SkeletonPoint()},
            {JointType.HandLeft, new SkeletonPoint()},
        };
        private Dictionary<JointType, SkeletonPoint> lastPos = new Dictionary<JointType, SkeletonPoint>() {
            {JointType.HandRight, new SkeletonPoint()},
            {JointType.HandLeft, new SkeletonPoint()},
        };
        private Dictionary<JointType, SkeletonPoint> lastKeyPos = new Dictionary<JointType, SkeletonPoint>() {
            {JointType.HandRight, new SkeletonPoint()},
            {JointType.HandLeft, new SkeletonPoint()},
        };
        private DateTime lastKeyTime;
        private enum Gesture : int { none, left_push };
        private Gesture currentTrack = Gesture.none;
        private int TOLERANCE = 20;
        private int MINDIST = 5;
        #endregion vars

        #region button helpers

        private bool is_within(SkeletonPoint dip, double top, double left, double height, double width)
        {
            return top <= dip.Y && dip.Y <= (top + height) && left <= dip.X && dip.X <= (left + width);
        }
        private bool on_button(Button b, SkeletonPoint dip)
        {
            return is_within(dip, Canvas.GetTop(b), Canvas.GetLeft(b), b.Height, b.Width);
        }
        private Button select_button(SkeletonPoint dip)
        {
            if (on_button(returnButton, dip)) return returnButton;
            if (on_button(saveButton, dip)) return saveButton;
            if (on_button(presentButton, dip)) return presentButton;
            return null;
        }
        private void button_click(Button b)
        {
            if (b == returnButton)
            {
                //HomePage home = new HomePage(kinectSensorChooser1);
                //this.NavigationService.Navigate(home);
            }
            else if (b == saveButton)
            {
            }
            else if (b == presentButton)
            {
                PresentationPage myPresentation = new PresentationPage(kinectSensorChooser1);
                this.NavigationService.Navigate(myPresentation);
            }
        }
        #endregion button helpers

        #region gesture helpers
        private bool is_push(JointType jt)
        {
            return currentPos[jt].Z < lastPos[jt].Z
                && Math.Abs(currentPos[jt].X - lastPos[jt].X) < TOLERANCE * 2
                && Math.Abs(currentPos[jt].Y - lastPos[jt].Y) < TOLERANCE * 2;
        }
        private bool is_swipe_left(JointType jt)
        {
            return currentPos[jt].X < lastPos[jt].X - MINDIST
                && Math.Abs(currentPos[JointType.HandRight].Y - lastPos[JointType.HandRight].Y) < TOLERANCE;
        }
        private bool is_swipe_up(JointType jt)
        {
            return currentPos[jt].Y < lastPos[jt].Y - MINDIST
                && Math.Abs(currentPos[JointType.HandRight].X - lastPos[JointType.HandRight].X) < TOLERANCE;
        }
        private bool is_swipe_down(JointType jt)
        {
            return currentPos[jt].Y > lastPos[jt].Y + MINDIST
                && Math.Abs(currentPos[JointType.HandRight].X - lastPos[JointType.HandRight].X) < TOLERANCE;
        }
        private bool isStill(JointType jt)
        {
            return Math.Abs(currentPos[jt].X - lastPos[jt].X) < TOLERANCE
                && Math.Abs(currentPos[jt].Y - lastPos[jt].Y) < TOLERANCE
                && Math.Abs(currentPos[jt].Z - lastPos[jt].Z) < TOLERANCE;
        }
        #endregion gesture helpers

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //kinectSensorChooser1.Kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
        }
    }
}
