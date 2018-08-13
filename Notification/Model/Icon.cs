using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Notification.Model
{
    public class Icon
    {
        /// <summary>
        /// An image to use as the icon, will be resized automatically.
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// Padding between the image and the notification border.
        /// </summary>
        public int Padding { get; set; }

        /// <summary>
        /// Color for the icon background
        /// </summary>
        public Color BackColor { get; set; }
    }
}
