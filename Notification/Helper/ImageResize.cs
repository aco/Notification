using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Notification.Model;

namespace Notification.Helper
{
    public class ImageResize
    {
        /// <summary>
        /// Scales an image to given dimensions, preserving aspect ratio
        /// </summary>
        /// <param name="icon">Subject icon</param>
        /// <param name="width">Desired width</param>
        /// <param name="height">Desired height</param>
        /// <returns>System.Drawing.Image</returns>
        public static Image ScaleImage(Model.Icon icon, int width, int height)
        {
            var adjustX = (double)width / icon.Image.Width;
            var adjustY = (double)height / icon.Image.Height;

            var ratio = Math.Min(adjustX, adjustY);

            var newWidth = (int)(icon.Image.Width * ratio) - icon.Padding;
            var newHeight = (int)(icon.Image.Height * ratio) - icon.Padding;

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(icon.Image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}
