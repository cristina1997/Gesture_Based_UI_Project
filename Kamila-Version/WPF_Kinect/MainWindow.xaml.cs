using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Requires adding Reference to Assemblies/Extensions/Microsoft.Kinect 2.0
using Microsoft.Kinect;

//References:http://kinect.github.io/tutorial/lab03/index.html, http://www.indiana.edu/~dcnlab/KinectWorkshop/Page7.html

namespace WPF_Kinect
{
    //Create an enum to define the currently selected mode. The code will now need to handle color, depth and infrared mode.
    public enum Mode
    {
        Color,
        Depth,
        Infrared
    }

    public partial class MainWindow : Window
    {
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;
        bool _displayBody = false;

        //Color is declared as default at the start
        Mode _mode = Mode.Color;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            // Get a reference to the multi-frame
            var reference = e.FrameReference.AcquireFrame();

            // Open color frame
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    // Do something with the frame...
                    if (_mode == Mode.Color)
                    {
                        camera.Source = ToBitmap(frame);
                    }
                }

            }

            // Open depth frame
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    // Do something with the frame...
                    if (_mode == Mode.Depth)
                    {
                        camera.Source = ToBitmap(frame);
                    }
                }
            }

            // Open infrared frame
            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    // Do something with the frame...
                    if (_mode == Mode.Infrared)
                    {
                        camera.Source = ToBitmap(frame);
                    }
                }
            }
            // Open body frame
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    // Do something with the body frame...
                    canvas.Children.Clear();

                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    foreach (var body in _bodies)
                    {
                        if (body.IsTracked)
                        {
                            DrawSkeleton(body);
                        }
                    }
                }
            }
        }

        public void DrawSkeleton(Body body)
        {
            if (body == null) return;

            // Draw the joints
            foreach (Joint joint in body.Joints.Values)
            {
                DrawJoint(joint);
            }

            // Draw the bones
            DrawLine(body.Joints[JointType.Head], body.Joints[JointType.Neck]);
            DrawLine(body.Joints[JointType.Neck], body.Joints[JointType.SpineShoulder]);
            DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.ShoulderLeft]);
            DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.ShoulderRight]);
            DrawLine(body.Joints[JointType.SpineShoulder], body.Joints[JointType.SpineMid]);
            DrawLine(body.Joints[JointType.ShoulderLeft], body.Joints[JointType.ElbowLeft]);
            DrawLine(body.Joints[JointType.ShoulderRight], body.Joints[JointType.ElbowRight]);
            DrawLine(body.Joints[JointType.ElbowLeft], body.Joints[JointType.WristLeft]);
            DrawLine(body.Joints[JointType.ElbowRight], body.Joints[JointType.WristRight]);
            DrawLine(body.Joints[JointType.WristLeft], body.Joints[JointType.HandLeft]);
            DrawLine(body.Joints[JointType.WristRight], body.Joints[JointType.HandRight]);
            DrawLine(body.Joints[JointType.HandLeft], body.Joints[JointType.HandTipLeft]);
            DrawLine(body.Joints[JointType.HandRight], body.Joints[JointType.HandTipRight]);
            DrawLine(body.Joints[JointType.HandTipLeft], body.Joints[JointType.ThumbLeft]);
            DrawLine(body.Joints[JointType.HandTipRight], body.Joints[JointType.ThumbRight]);
            DrawLine(body.Joints[JointType.SpineMid], body.Joints[JointType.SpineBase]);
            DrawLine(body.Joints[JointType.SpineBase], body.Joints[JointType.HipLeft]);
            DrawLine(body.Joints[JointType.SpineBase], body.Joints[JointType.HipRight]);
            DrawLine(body.Joints[JointType.HipLeft], body.Joints[JointType.KneeLeft]);
            DrawLine(body.Joints[JointType.HipRight], body.Joints[JointType.KneeRight]);
            DrawLine(body.Joints[JointType.KneeLeft], body.Joints[JointType.AnkleLeft]);
            DrawLine(body.Joints[JointType.KneeRight], body.Joints[JointType.AnkleRight]);
            DrawLine(body.Joints[JointType.AnkleLeft], body.Joints[JointType.FootLeft]);
            DrawLine(body.Joints[JointType.AnkleRight], body.Joints[JointType.FootRight]);

        }

        public void DrawJoint(Joint joint)
        {
            if (joint.TrackingState == TrackingState.Tracked)
            {
                // 3D space point
                CameraSpacePoint jointPosition = joint.Position;

                // 2D space point
                Point point = new Point();

                ColorSpacePoint colorPoint = _sensor.CoordinateMapper.MapCameraPointToColorSpace(jointPosition);

                // Handle inferred points
                point.X = float.IsInfinity(colorPoint.X) ? 0 : colorPoint.X;
                point.Y = float.IsInfinity(colorPoint.Y) ? 0 : colorPoint.Y;

                // Draw an ellipse for that joint
                Ellipse ellipse = new Ellipse { Fill = Brushes.Yellow, Width = 40, Height = 40 };

                Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);

                canvas.Children.Add(ellipse);
            }
        }

        public void DrawLine(Joint first, Joint second)
        {
            if (first.TrackingState == TrackingState.NotTracked || second.TrackingState == TrackingState.NotTracked) return;

            // Joint data is in Camera XYZ coordinates
            // 3D space point
            CameraSpacePoint jointFirstPosition = first.Position;
            CameraSpacePoint jointSecondPosition = second.Position;

            // 2D space points in XY coordinates
            Point pointFirst = new Point();
            Point pointSecond = new Point();

            // Apply COORDINATE MAPPING - Here mapping to ColorSpace
            ColorSpacePoint colorPointFirst = _sensor.CoordinateMapper.MapCameraPointToColorSpace(jointFirstPosition);
            ColorSpacePoint colorPointSecond = _sensor.CoordinateMapper.MapCameraPointToColorSpace(jointSecondPosition);

            // Handle inferred points
            pointFirst.X = float.IsInfinity(colorPointFirst.X) ? 0 : colorPointFirst.X;
            pointFirst.Y = float.IsInfinity(colorPointFirst.Y) ? 0 : colorPointFirst.Y;

            pointSecond.X = float.IsInfinity(colorPointSecond.X) ? 0 : colorPointSecond.X;
            pointSecond.Y = float.IsInfinity(colorPointSecond.Y) ? 0 : colorPointSecond.Y;

            // Creat a Line using the ColorSpacePoints
            Line line = new Line
            {
                X1 = pointFirst.X,
                Y1 = pointFirst.Y,
                X2 = pointSecond.X,
                Y2 = pointSecond.Y,
                StrokeThickness = 8,
                Stroke = new SolidColorBrush(Colors.Yellow)
            };

            canvas.Children.Add(line);
        }

        // On color button click change camera to color mode
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Color;
        }

        // On depth button click change camera to depth mode
        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Depth;
        }

        // On infrared button click change camera to infrared mode
        private void Infrared_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Infrared;
        }
        private void Body_Click(object sender, RoutedEventArgs e)
        {
            _displayBody = !_displayBody;
        }


        // Convert a ColorFrame to an ImageSource
        private ImageSource ToBitmap(ColorFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            byte[] pixels = new byte[width * height * ((format.BitsPerPixel + 7) / 8)];

            if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
            {
                frame.CopyRawFrameDataToArray(pixels);
            }
            else
            {
                frame.CopyConvertedFrameDataToArray(pixels, ColorImageFormat.Bgra);
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);
        }

        // Convert a DepthFrame to an ImageSource
        private ImageSource ToBitmap(DepthFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            ushort minDepth = frame.DepthMinReliableDistance;
            ushort maxDepth = frame.DepthMaxReliableDistance;

            ushort[] depthData = new ushort[width * height];
            byte[] pixelData = new byte[width * height * (format.BitsPerPixel + 7) / 8];

            frame.CopyFrameDataToArray(depthData);

            int colorIndex = 0;
            for (int depthIndex = 0; depthIndex < depthData.Length; ++depthIndex)
            {
                ushort depth = depthData[depthIndex];
                byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                pixelData[colorIndex++] = intensity; // Blue
                pixelData[colorIndex++] = intensity; // Green
                pixelData[colorIndex++] = intensity; // Red

                ++colorIndex;
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixelData, stride);
        }

        // Convert an InfraredFrame to an ImageSource
        private ImageSource ToBitmap(InfraredFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            ushort[] infraredData = new ushort[width * height];
            byte[] pixelData = new byte[width * height * (format.BitsPerPixel + 7) / 8];

            frame.CopyFrameDataToArray(infraredData);

            int colorIndex = 0;
            for (int infraredIndex = 0; infraredIndex < infraredData.Length; ++infraredIndex)
            {
                ushort ir = infraredData[infraredIndex];
                byte intensity = (byte)(ir >> 8);

                pixelData[colorIndex++] = intensity; // Blue
                pixelData[colorIndex++] = intensity; // Green   
                pixelData[colorIndex++] = intensity; // Red

                ++colorIndex;
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixelData, stride);
        }
    }
}