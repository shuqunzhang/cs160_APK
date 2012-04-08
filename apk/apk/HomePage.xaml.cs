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
using Coding4Fun.Kinect.Wpf;
using Microsoft.Samples.Kinect.WpfViewers;
using System.Windows.Threading;

namespace apk
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public HomePage(KinectSensorChooser ks) 
        {
            InitializeComponent();
            kinectSensorChooser1 = ks;
            startUp = false;
        }

        private bool startUp = true;

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
            if (on_button(beginButton, dip)) return beginButton;
            if (on_button(reviewButton, dip)) return reviewButton;
            if (on_button(settingsButton, dip)) return settingsButton;
            if (on_button(closeButton, dip)) return closeButton;
            return null;
        }
        private void button_click(Button b)
        {
            if (b == settingsButton)
            {
                SettingsPage settings = new SettingsPage(kinectSensorChooser1);
                this.NavigationService.Navigate(settings);
            }
            else if (b == closeButton)
            {
                Application curApp = Application.Current;
                curApp.Shutdown();
            }
            else if (b == beginButton) 
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
            if (startUp)
            {
                kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);
            }
            else
            {
                kinectSensorChooser1.Kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            }
        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor old = (KinectSensor)e.OldValue;

            StopKinect(old);

            KinectSensor sensor = (KinectSensor)e.NewValue;

            if (sensor == null)
            {
                return;
            }

            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
            sensor.SkeletonStream.Enable(parameters);

            sensor.SkeletonStream.Enable();

            sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            try
            {
                sensor.Start();
            }
            catch (System.IO.IOException)
            {
                kinectSensorChooser1.AppConflictOccurred();
            }
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (closing||e==null)
            {
                return;
            }

            //Get a skeleton
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
            {
                return;
            }


            //set scaled position
            ScalePosition(cursor, first.Joints[JointType.HandRight]);
            ScalePosition(nothing, first.Joints[JointType.HandLeft]);

            //GetCameraPoint(first, e);

            //LOGIC GOES HERE
            //currentPos will be set in GetCameraPoint / ScalePosition

            TimeSpan timeDifference = DateTime.Now - lastKeyTime;

            //not important, I just didn't want to keep typing JointType.blah over and over
            JointType right = JointType.HandRight;
            JointType left = JointType.HandLeft;

            //debugging
            debug.Content = "";

            //for styling buttons on hover,etc
            beginButton.Tag = "";
            reviewButton.Tag = "";
            settingsButton.Tag = "";
            closeButton.Tag = "";
            if (on_button(closeButton, currentPos[right]))
            { //hovering button4
                closeButton.Tag = "hover";
            }

            bool resetTrack = true;
            switch (currentTrack)
            {
                case Gesture.none:
                    resetTrack = false;
                    if (is_push(left))
                    { //push w/ left hand
                        currentTrack = Gesture.left_push;
                        lastKeyTime = DateTime.Now;
                        lastKeyPos[JointType.HandLeft] = currentPos[JointType.HandLeft];
                    }
                    break;

                case Gesture.left_push:
                    if (is_push(left))
                    {
                        resetTrack = false;
                        if (lastKeyPos[left].Z - currentPos[left].Z > 0.15)
                        {
                            Button b = select_button(currentPos[right]);
                            if (b != null)
                            {
                                b.Tag = "press";
                                if (b.IsEnabled) button_click(b);
                                resetTrack = true;
                            }
                        }
                    }
                    break;
            }
            if (resetTrack)
            {
                currentTrack = Gesture.none;
                lastKeyTime = DateTime.Now;
            }
            lastPos[JointType.HandRight] = currentPos[JointType.HandRight];
            lastPos[JointType.HandLeft] = currentPos[JointType.HandLeft];
        }

        #region kinectHelpers
        void GetCameraPoint(Skeleton first, AllFramesReadyEventArgs e)
        {

            using (DepthImageFrame depth = e.OpenDepthImageFrame())
            {
                if (depth == null ||
                    kinectSensorChooser1.Kinect == null)
                {
                    return;
                }

                //Map a joint location to a point on the depth map
                //left hand
                DepthImagePoint leftDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HandLeft].Position);
                //right hand
                DepthImagePoint rightDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HandRight].Position);


                //Map a depth point to a point on the color image
                //left hand
                ColorImagePoint leftColorPoint =
                    depth.MapToColorImagePoint(leftDepthPoint.X, leftDepthPoint.Y,
                    ColorImageFormat.RgbResolution640x480Fps30);
                //right hand
                ColorImagePoint rightColorPoint =
                    depth.MapToColorImagePoint(rightDepthPoint.X, rightDepthPoint.Y,
                    ColorImageFormat.RgbResolution640x480Fps30);

                //Set location
                //CameraPosition(leftEllipse, leftColorPoint);
                CameraPosition(cursor, rightColorPoint);

                //currentPos[JointType.HandRight] = rightDepthPoint;
                //currentPos[JointType.HandLeft] = leftDepthPoint;
            }
        }

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

        private void CameraPosition(FrameworkElement element, ColorImagePoint point)
        {
            //Divide by 2 for width and height so point is right in the middle 
            // instead of in top/left corner
            Canvas.SetLeft(element, point.X - element.Width / 2);
            Canvas.SetTop(element, point.Y - element.Height / 2);

        }

        private void ScalePosition(FrameworkElement element, Joint joint)
        {
            //convert the value to X/Y
            //Joint scaledJoint = joint.ScaleTo(1280, 720); 

            //convert & scale (.3 = means 1/3 of joint distance)
            Joint scaledJoint = joint.ScaleTo(1080, 700, .3f, .3f);

            currentPos[joint.JointType] = scaledJoint.Position;
            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y);

        }
#endregion kinectHelpers

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    //stop sensor 
                    sensor.Stop();

                    //stop audio if not null
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }


                }
            }
        }

        public void StopKinectHack()
        {
            KinectSensor sensor = kinectSensorChooser1.Kinect;
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    //stop sensor 
                    sensor.Stop();

                    //stop audio if not null
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }


                }
            }
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    closing = true;
        //    StopKinect(kinectSensorChooser1.Kinect);
        //}

    }
}
