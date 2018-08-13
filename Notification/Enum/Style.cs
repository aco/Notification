using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Enum
{
    public enum Style
    {
        /// <summary>
        /// Slides out from edge when showing and slides into edge when hiding.
        /// </summary>
        /// <remarks>
        /// Requires a direction.
        /// </remarks>
        Slide = 0x40000,

        /// <summary>
        /// Fades from transaprent to opaque when showing and from opaque to transparent when hiding.
        /// </summary>
        Fade = 0x80000
    }
}
