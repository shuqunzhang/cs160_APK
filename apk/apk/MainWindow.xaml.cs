﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        private static List<Button> buttonList;
        public static KinectSensorChooser ksc;
        public enum PageType : int { home, settings, presentation, result, review, gestures, words };
        private static Page currentPage;
        private static PageType currentPageType;
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
        public static DateTime loadedTime;
        public static DateTime lastNoteTime;
        private DateTime lastKeyTime;
        private enum Gesture : int { none, left_push, ending};
        private Gesture currentTrack = Gesture.none;
        private int TOLERANCE = 20;
        private int MINDIST = 5;
        #endregion variables
        public MainWindow()
        {
            InitializeComponent();
            buttonList = new List<Button>();
            ksc = kinectSensorChooser1;
        }

        public static void updateButtons(List<Button> bl) 
        {
            Button oldButton;
            Button newButton;
            for (int i = 0; i < buttonList.Count; i++) 
            {
                oldButton = buttonList.ElementAt(i);
                oldButton.Visibility = Visibility.Hidden;
                oldButton.IsEnabled = false;
            }
            for (int i = 0; i < bl.Count; i++)
            {
                newButton = bl[i];
                newButton.Visibility = Visibility.Visible;
                newButton.IsEnabled = true;
            }
            buttonList = bl;
        }
        public static void setCurrentPageType(PageType p)
        {
            currentPageType = p;
        }
        public static void setCurrentPage(Page p)
        {
            currentPage = p;
        }

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
            for (int i = 0; i < buttonList.Count; i++)
            {
                Button aButton = buttonList[i];
                if (on_button(aButton, dip))
                {
                    return aButton;
                }
            }
            return null;
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

        public static void closeApplication() 
        {
            closeKinect(ksc.Kinect);
            Application curApp = Application.Current;
            curApp.Shutdown();
        }

        public static void closeKinect(KinectSensor sensor)
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
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ksc.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopKinect(ksc.Kinect);
        }
        private void blank_Loaded(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            blank.NavigationService.Navigate(homePage);
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
                ksc.AppConflictOccurred();
            }
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            if (closing)
            {
                return;
            }

            //Get a skeleton
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
            {
                return;
            }


            //not important, I just didn't want to keep typing JointType.blah over and over
            JointType right = JointType.HandRight;
            JointType left = JointType.HandLeft;
            JointType head = JointType.Head;

            //set scaled position
            ScalePosition(cursor, first.Joints[JointType.HandRight]);
            ScalePosition(nothing, first.Joints[JointType.HandLeft]);
            ScalePosition(nothing, first.Joints[JointType.Head]);

            //GetCameraPoint(first, e);

            //LOGIC GOES HERE
            //currentPos will be set in GetCameraPoint / ScalePosition

            TimeSpan timeDifference = DateTime.Now - lastKeyTime;

            //debugging
            //debug.Content = "";

            //for styling buttons on hover,etc
            Button selectedButton = select_button(currentPos[right]);
            if (selectedButton != null)
            {
                selectedButton.Tag = "hover";
            }

            bool resetTrack = true;
            switch (currentPageType)
            {
                case PageType.home:
                case PageType.result:
                    #region Home
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
                                    if (selectedButton != null)
                                    {
                                        selectedButton.Tag = "press";
                                        if (selectedButton.IsEnabled)
                                        {
                                            selectedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                        }
                                        resetTrack = true;
                                    }
                                }
                            }
                            break;
                    }
                    #endregion Home
                    break;


                case PageType.settings:
                    #region Settings
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
                                    if (selectedButton != null)
                                    {
                                        selectedButton.Tag = "press";
                                        if (selectedButton.IsEnabled)
                                        {
                                            selectedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                        }
                                        resetTrack = true;
                                    }
                                }
                            }
                            break;
                    }
                    #endregion Settings
                    break;


                case PageType.presentation:
                    #region Presentation
                    PresentationPage pp = (PresentationPage)currentPage;
                    if ((DateTime.Now - loadedTime).Seconds < 5)
                    {
                        pp.changeCount(5 - (DateTime.Now - loadedTime).Seconds);
                    }
                    pp.finishCount();
                    switch (currentTrack)
                    {
                        case Gesture.none:
                            resetTrack = false;
                            if (currentPos[right].Y < currentPos[head].Y && currentPos[left].Y < currentPos[head].Y)
                            { //both hands over head
                                currentTrack = Gesture.ending;
                                lastKeyTime = DateTime.Now;
                                lastKeyPos[left] = currentPos[left];
                                lastKeyPos[right] = currentPos[right];
                                lastKeyPos[head] = currentPos[head];
                            }
                            break;

                        case Gesture.ending:
                            if (isStill(head) && isStill(right) && isStill(left))
                            {
                                resetTrack = false;
                                if ((DateTime.Now - lastKeyTime).Seconds > 3)
                                {
                                    ResultPage result = new ResultPage();
                                    blank.NavigationService.Navigate(result);
                                    resetTrack = true;
                                }
                            }
                            break;
                    }
                    #endregion Presentation
                    break;
            }
            
            if (resetTrack)
            {
                currentTrack = Gesture.none;
                lastKeyTime = DateTime.Now;
            }
            lastPos[JointType.HandRight] = currentPos[JointType.HandRight];
            lastPos[JointType.HandLeft] = currentPos[JointType.HandLeft];
            lastPos[JointType.Head] = currentPos[JointType.Head];
        }

        #region kinectHelpers
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

        
    }
}
