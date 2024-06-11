using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryImageList imgList; 
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        private BitmapSource originalImage;
        private bool isBlackAndWhite = false;

        public MainWindow()
        {
            InitializeComponent();
            resetList();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            setPath();
        }

        private void setPath() 
        { 
            FolderBrowserDialog dlg = new FolderBrowserDialog(); 
            dlg.ShowDialog(); 
            path = dlg.SelectedPath; 
            resetList(); 
        }

        private void resetList() 
        { 
            if (isValidPath(path)) 
            { 
                imgList = new DirectoryImageList(path); 
            } 
            this.DataContext = imgList.Images; 

        }
        private bool isValidPath(string path) 
        { 
            try 
            { 
                string folder = System.IO.Path.GetFullPath(path); 
                return true; 
            } 
            catch { 
                return false; 
            } 
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            originalImage = (BitmapSource)((sender as System.Windows.Controls.ListBox).SelectedItem);
            image1.Source = originalImage;
            isBlackAndWhite = false;
        }

        private void btnBW_Click(object sender, RoutedEventArgs e)
        {
            if (originalImage == null) return;

            if (isBlackAndWhite)
            {
                image1.Source = originalImage;
                isBlackAndWhite = false;
            }
            else
            {
                BitmapSource img = (BitmapSource)image1.Source;
                image1.Source = new FormatConvertedBitmap(img, PixelFormats.Gray16, BitmapPalettes.Gray256, 1.0);
                isBlackAndWhite = true;
            }
        }

        private void btnBlur_Click(object sender, RoutedEventArgs e)
        {
            if (image1.Effect != null)
            { 
                image1.Effect = null;
            } 
            else 
            { 
                image1.Effect = new BlurEffect(); 
            }
        }

        private void btnRot_Click(object sender, RoutedEventArgs e)
        {
            CachedBitmap cache = new CachedBitmap((BitmapSource)image1.Source, 
                BitmapCreateOptions.None, BitmapCacheOption.OnLoad); 
            image1.Source = new TransformedBitmap(cache, new RotateTransform(90));
        }

        private void btnFlip_Click(object sender, RoutedEventArgs e)
        {
            CachedBitmap cache = new CachedBitmap((BitmapSource)image1.Source, 
                BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            ScaleTransform scale = new ScaleTransform(-1, -1, image1.Source.Width / 2, image1.Source.Height / 2); 
            image1.Source = new TransformedBitmap(cache, scale);
        }
    }
}