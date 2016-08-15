using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace Lol_Account_Changer
{
    public class PixelSearch
    {
        private bool _ColorFound = false;
        public bool ColorFound
        {
            get
            {
                return _ColorFound;
            }
        }
        public Point GetPixelPosition(Color SearchColor, bool IgnoreAlphaChannel)
        {
            _ColorFound = false;
            Point PixelPt = new Point(0, 0);
            using (Bitmap b = CaptureScreen())
            {
                for (int i = 0; i < b.Width; i++)
                {
                    if (this._ColorFound)
                        break;
                    for (int j = 0; j < b.Height; j++)
                    {
                        if (this._ColorFound)
                            break;
                        Color tmpPixelColor = b.GetPixel(i, j);
                        if (((tmpPixelColor.A == SearchColor.A) || IgnoreAlphaChannel)
                            && (tmpPixelColor.R == SearchColor.R)
                            && (tmpPixelColor.G == SearchColor.G)
                            && (tmpPixelColor.B == SearchColor.B)
                            )
                        {
                            PixelPt.X = i;
                            PixelPt.Y = j;
                            this._ColorFound = true;
                        }
                    }
                }
            }
            return PixelPt;
        }
        private Bitmap CaptureScreen()
        {
            Bitmap b = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), b.Size);
            }
            return b;
        }
    }
}
