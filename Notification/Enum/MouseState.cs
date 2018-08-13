using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Enum
{
    public enum MouseState
    {
        /// <summary>
        /// Mouse not interacting with the area.
        /// </summary>
        None,

        /// <summary>
        /// Mouse presently over the area.
        /// </summary>
        Over,

        /// <summary>
        /// Mouse depressed inside the area.
        /// </summary>
        Down
    }
}
