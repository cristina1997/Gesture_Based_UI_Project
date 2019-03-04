using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

using Microsoft.Kinect;

namespace WPF_Kinect
{
    public enum DisplayFrameType
    {
        Infrared, Color
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private KinectSensor kinectSensor = null;
        private const DisplayFrameType DEFAULT_DISPLAYTYPE = DisplayFrameType.Color;


        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Unloaded += MainWindow_Unloaded;
        }

        #region Page Loaded, Unloaded Events, Setup Events
        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if( kinectSensor != null && kinectSensor.IsOpen)
            {
                kinectSensor.Close();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.kinectSensor = KinectSensor.GetDefault();
            //if(this.kinectSensor != null)
            //{
            //    this.kinectSensor.Open();
            //    if (kinectSensor.IsOpen)
            //    {
            //        this.StatusText = "Kinect successfully acquired and opened!";
            //    }
            //    else
            //    {
            //        this.StatusText = "Cannot open kinect";
            //    }
            //}
        }

        private void SetupCurrentDisplay(DisplayFrameType newDisplayType)
        {
            switch (newDisplayType)
            {
                case DisplayFrameType.Infrared:
                    break;
                case DisplayFrameType.Color:
                    break;
                default:
                    break;
            }
        }


        #endregion

        #region Public Properties with methods
        private DisplayFrameType currentDisplayFrameType;

        private string statusText = null;
        public string StatusText
        {
            get { return statusText; }
            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    OnPropertyChanged();
                }
            }
        }

        private FrameDescription currentFrameDescription;
        public FrameDescription CurrentFrameDescription
        {
            get { return this.currentFrameDescription; }
            set
            {
                if (this.currentFrameDescription != value)
                {
                    this.currentFrameDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Colour Image Setup


        #endregion


        #region Infrared Imaging setup

        private const int BytesPerPixel = 4;

        private WriteableBitmap bitmap = null;

        // == Infrared Frame Reader and buffers ==
        private InfraredFrameReader InfraredFrameReader = null;
        private ushort[] infraredFrameData = null;
        private byte[] infraredPixels = null;

        // == const values for use in maths around conversion from
        //    infrared to pixel ==

        // == this is 65535
        private const float InfraredSourceValueMaximum = (float)ushort.MaxValue;

        // lower and upper limits of the fram values that are rendered
        // 
        private const float InfraredOutputValueMinimum = 0.01f;

        private const float InfraredOutputValueMaximum = 1.0f;

        // average value of infrared based on research
        private const float InfraredSceneValueAverage = 0.08f;


        private const float InfraredSceneStandardDeviation = 3.0f;

        private void btnInfrared_Click(object sender, RoutedEventArgs e)
        {
            // set up the infrared frame data
            FrameDescription infraredFrameDescription = this.kinectSensor.InfraredFrameSource.FrameDescription;

            this.InfraredFrameReader = this.kinectSensor.InfraredFrameSource.OpenReader();

            this.InfraredFrameReader.FrameArrived += InfraredFrameReader_FrameArrived;

            this.infraredFrameData = new ushort[infraredFrameDescription.Width * infraredFrameDescription.Height];
            this.infraredPixels = new byte[infraredFrameDescription.Width * infraredFrameDescription.Height * BytesPerPixel];

            this.bitmap = new WriteableBitmap(infraredFrameDescription.Width, 
                                              infraredFrameDescription.Height,
                                              96, 96, 
                                              PixelFormats.Bgra32, null);
            
            this.CurrentFrameDescription = infraredFrameDescription;

            this.kinectSensor.IsAvailableChanged += KinectSensor_IsAvailableChanged;

            this.DataContext = this;

            this.kinectSensor.Open();

        }

        private void InfraredFrameReader_FrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            bool infraredFrameProcessed = false;

            using (InfraredFrame irFrame = e.FrameReference.AcquireFrame())
            {
                if(irFrame != null)
                {
                    FrameDescription irFrameDescription = irFrame.FrameDescription;
                    if( ( (irFrameDescription.Width * irFrameDescription.Height) == this.infraredFrameData.Length) && 
                        (irFrameDescription.Width == this.bitmap.Width) &&
                        (irFrameDescription.Height == this.bitmap.Height) )
                    {
                        // copy the pixel data from the image ot a temp array
                        irFrame.CopyFrameDataToArray(this.infraredFrameData);
                        infraredFrameProcessed = true;
                    }
                }
            }

            if(infraredFrameProcessed)
            {
                ConvertInfraredDataToPixels();
                RenderPixelArray(this.infraredPixels);
            }
        }

        private void ConvertInfraredDataToPixels()
        {
            int colorPixelIndex = 0;
            for (int i = 0; i < this.infraredFrameData.Length; i++)
            {
                float intensityRatio = (float)this.infraredFrameData[i] / InfraredSourceValueMaximum;

                intensityRatio /= InfraredSceneValueAverage * InfraredSceneStandardDeviation;

                intensityRatio = Math.Min(InfraredOutputValueMaximum, intensityRatio);
                intensityRatio = Math.Max(InfraredOutputValueMinimum, intensityRatio);

                byte intensity = (byte)(intensityRatio * 255.0f);
                this.infraredPixels[colorPixelIndex++] = intensity; // Blue
                this.infraredPixels[colorPixelIndex++] = intensity; // Red
                this.infraredPixels[colorPixelIndex++] = intensity; // Green
                this.infraredPixels[colorPixelIndex++] = 255;   // Alpha
            }
        }

        private void RenderPixelArray(byte[] pixels)
        {
            Int32Rect rect = new Int32Rect(0,
                                           0, 
                                           this.CurrentFrameDescription.Width, 
                                           this.CurrentFrameDescription.Height);
            // rect - the rectangle being written to
            // pixels - the source array for the data
            // stride - number of the pixels array per row
            // offset - an offset to start at in the pixels array
            bitmap.WritePixels(rect, pixels, this.currentFrameDescription.Width * BytesPerPixel, 0);

            FrameDisplayImage.Source = this.bitmap;
        }
        #endregion

        private void KinectSensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            this.StatusText = this.kinectSensor.IsAvailable ? "Running" : "Not Available";
        }


    }
}
