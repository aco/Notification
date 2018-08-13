using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

using Notification.Enum;
using Notification.Model;

namespace Notification.Control
{
    public class DismissButton : System.Windows.Forms.Control
    {
        private const string CLOSE_TEXT = "X";

        private MouseState _mouseState;
        private Brush _defaultBrush;

        /// <summary>
        /// Initializes the dismiss button.
        /// </summary>
        /// <param name="defaultBrush">Default color of the close button when not hovered or pressed.</param>
        public DismissButton(Color iconColor)
        {
            _defaultBrush = new SolidBrush(iconColor);
            _mouseState = MouseState.None;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            this.Size = new Size(12, 12);
            this.Font = new Font(this.Font.Name, 10, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            switch (_mouseState)
            {
                case MouseState.Over:
                    e.Graphics.DrawString(CLOSE_TEXT, this.Font, Brushes.Red, 0, 0);
                    break;
                case MouseState.Down:
                    e.Graphics.DrawString(CLOSE_TEXT, this.Font, Brushes.DarkRed, 0, 0);
                    break;
                default:
                    e.Graphics.DrawString(CLOSE_TEXT, this.Font, _defaultBrush, 0, 0);
                    break;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.FindForm().Close();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseState = MouseState.Over;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseState = MouseState.None;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseState = MouseState.Down;
            this.Invalidate();
        }
    }
}
