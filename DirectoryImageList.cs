using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
    public class DirectoryImageList
    {
        private string path; 
        private List<BitmapSource> images = new List<BitmapSource>(); 
        public DirectoryImageList(string path) { 
            this.path = path; 
            loadImages(); 
        }
        public List<BitmapSource> Images { 
            get { return images; } 
            set { images = value; } 
        }
        public string Path { 
            get { return path; } 
            set { path = value; 
                loadImages(); 
            } 
        }
        private void loadImages()
        {
            images.Clear(); 
            BitmapImage img; 
            
            Array.ForEach(Directory.GetFiles(path), f => {
                try { 
                    img = new BitmapImage(new Uri(f)); 
                    images.Add(img); 
                }
                catch {
                }
            });
        }
    }
}
