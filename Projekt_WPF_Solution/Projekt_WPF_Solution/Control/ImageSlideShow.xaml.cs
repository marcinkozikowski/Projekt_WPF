using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Projekt_WPF_Solution.Control
{
    /// <summary>
    /// Interaction logic for ImageSlideShow.xaml
    /// </summary>
    public partial class ImageSlideShow : UserControl
    {
        Image[] imageControls;
        DispatcherTimer imageSlideTimer;
        private List<BitmapImage> imageSources = new List<BitmapImage>();
        int currentSourceIndex, currentImageControlIndex;

        public ImageSlideShow()
        {
            InitializeComponent();
            imageControls = new[] { image1, image2 };
            

            imageSlideTimer = new DispatcherTimer();
            imageSlideTimer.Interval = new TimeSpan(0, 0, 5);
            imageSlideTimer.Tick += new EventHandler(imageSlideTimerTick);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            imageSources.Add(image1.Source as BitmapImage);
            imageSources.Add(image2.Source as BitmapImage);
            PlaySlideShow();
            imageSlideTimer.Start();
        }

        private void PlaySlideShow()
        {
            try
            {
                if (imageSources.Count == 0)
                { return; }
                int oldControlIndex = currentImageControlIndex;
                currentImageControlIndex = (currentImageControlIndex + 1) % imageSources.Count;
                currentSourceIndex = (currentSourceIndex + 1) % 2;

                Image fadeOut = imageControls[oldControlIndex];
                Image fadeIn = imageControls[currentImageControlIndex];
                ImageSource newSource = imageSources[currentSourceIndex];
                fadeIn.Source = newSource;

                Storyboard fadeOutStory = (Resources["FadeOut"] as Storyboard).Clone();
                fadeOutStory.Begin(fadeOut);
                Storyboard fadeInStory = (Resources["FadeIn"] as Storyboard);
                fadeInStory.Begin(fadeIn);

            }
            catch (Exception)
            { }
        }

        private void ImageSourceUpdated(object sender, DataTransferEventArgs e)
        {
            imageSlideTimer.Stop();
            imageSources.Clear();
            imageSources.Add(image1.Source as BitmapImage);
            imageSources.Add(image2.Source as BitmapImage);
            PlaySlideShow();
            imageSlideTimer.Start();

        }

        private void imageSlideTimerTick(object sender, EventArgs e)
        {
            PlaySlideShow();
        }

        

    }
}
