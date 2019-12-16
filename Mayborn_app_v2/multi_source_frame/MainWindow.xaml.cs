

namespace multi_source_frame
{
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

    using Microsoft.Kinect;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private KinectSensor sensor = null;
        private MultiSourceFrameReader reader = null;
        public MainWindow()
        {
            sensor = KinectSensor.GetDefault();
            reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);
            reader.MultiSourceFrameArrived += reader_multiSourceFrameArrived;
            InitializeComponent();
        }

        private void reader_multiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)

        {

            // Get a reference to the multi-frame    

            var reference = e.FrameReference.AcquireFrame();

            // Open color frame    

            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {

                if (frame != null)
                {

                    if (_mode == 0)
                    {
                        displayStream.Source = frame.ToBitmap();
                    }

                }

            }

            // Open depth frame    

            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {

                if (frame != null)
                {

                    if (_mode == 1)
                    {
                        displayStream.Source = frame.ToBitmap();
                    }

                }

            }

            // Open infrared frame    

            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {

                if (frame != null)
                {

                    if (_mode == 2)
                    {
                        displayStream.Source = frame.ToBitmap();
                    }

                }

            }
        }
        int _mode = 0;

        private void ColorStreamButton_Click(object sender, RoutedEventArgs e)
        {
            _mode = 0;
        }

        private void DepthStreamButton_Click(object sender, RoutedEventArgs e)
        {
            _mode = 1;
        }

        private void InfraredStreamButton_Click(object sender, RoutedEventArgs e)
        {
            _mode = 2;
        }
    }
}
