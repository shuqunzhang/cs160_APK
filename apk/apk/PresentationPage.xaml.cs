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

        public PresentationPage(KinectSensorChooser ks) 
        {
            InitializeComponent();
            kinectSensorChooser1 = ks;
            previous = DateTime.Now;
        }

        private DateTime previous;
        const int skeletonCount = 6;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            notice.Visibility = System.Windows.Visibility.Visible;
            timeRemaining.Visibility = System.Windows.Visibility.Visible;
            //kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);

            //This is to customize the action done when all frames are ready. 
            //(i.e. sets AllFramesReady to point to the one defined in this class)
            kinectSensorChooser1.Kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e) 
        {

            if (e == null)
            {
                return;
            }

            //Get a skeleton
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
            {
                return;
            }

            //if one second passed do something
            double timePassed = DateTime.Now.Subtract(previous).TotalMilliseconds;
            if (timePassed > 1000)
            {
                if (notice.Visibility == System.Windows.Visibility.Hidden)
                {
                    return;
                }
                int curSeconds = Convert.ToInt32(timeRemaining.Text);
                if (curSeconds > 0)
                {
                    timeRemaining.Text = (curSeconds - 1).ToString();
                }
                else if (curSeconds == 0)
                {
                    notice.Visibility = System.Windows.Visibility.Hidden;
                    timeRemaining.Visibility = System.Windows.Visibility.Hidden;
                    timeRemaining.Text = "5";
                }
                previous = DateTime.Now;
            }
        }

        #region sameOldGetFirstSkeleton
        Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return null;
                }


                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                //get the first tracked skeleton
                Skeleton first = (from s in allSkeletons
                                  where s.TrackingState == SkeletonTrackingState.Tracked
                                  select s).FirstOrDefault();

                return first;

            }
        }
        #endregion sameOldGetFirstSkeleton

        


        //void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    KinectSensor old = (KinectSensor)e.OldValue;

        //    StopKinect(old);

        //    KinectSensor sensor = (KinectSensor)e.NewValue;

        //    if (sensor == null)
        //    {
        //        return;
        //    }
        //    sensor.SkeletonStream.Enable();

        //    sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
        //    sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
        //    sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

        //    try
        //    {
        //        sensor.Start();
        //    }
        //    catch (System.IO.IOException)
        //    {
        //        kinectSensorChooser1.AppConflictOccurred();
        //    }
        //}

        //private void StopKinect(KinectSensor sensor)
        //{
        //    if (sensor != null)
        //    {
        //        if (sensor.IsRunning)
        //        {
        //            stop sensor 
        //            sensor.Stop();

        //            stop audio if not null
        //            if (sensor.AudioSource != null)
        //            {
        //                sensor.AudioSource.Stop();
        //            }

        //        }
        //    }
        //}
    }
}
