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
using Notification.Helper;

namespace Notification.Control
{
    public class Notification : Form
    {
        /// <summary>
        /// Title for the notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Color for the title.
        /// </summary>
        public Color TitleColor { get; set; }

        /// <summary>
        /// The body text of the notification.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// An image and padding for the notification.
        /// </summary>
        public new Model.Icon Icon { get; set; }

        /// <summary>
        /// Padding from the X and Y axis of the working screen area.
        /// </summary>
        public int ScreenPadding { get; set; }

        private Animator _animator;
        private Timer _lifeTimer;

        protected override void CreateHandle()
        {
            base.CreateHandle();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(380, 80);

            this.DoubleBuffered = true;

            var bounds = Screen.PrimaryScreen.WorkingArea;

            this.Location = new Point(bounds.Width -
                (this.Width + ScreenPadding), bounds.Height - (this.Height + ScreenPadding));

            var dismissButton = new DismissButton(Color.DarkRed)
            {
                Location = new Point(this.Width - 18, 5),
                Font = this.Font
            };

            this.Controls.Add(dismissButton);
            dismissButton.BringToFront();
        }

        /// <summary>
        /// Initializes the notification and starts animation.
        /// </summary>
        /// <param name="animation">Style of animation to play</param>
        /// <param name="animationDuration">Duration of the animation.</param>
        /// <param name="direction">Entrance direction. Exit direction is computed automatically.</param>
        public Notification(Style animation, int animationDuration, Direction direction = Direction.Left)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            this.TopMost = true;
            this.ShowInTaskbar = false;

            this.Shown += new EventHandler(Form_Shown);

            _animator = new Animator(this)
            {
                Animation = animation,
                Direction = direction,
                Duration = animationDuration
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            var formatter = new StringFormat()
            {
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.EllipsisCharacter
            };

            e.Graphics.Clear(this.BackColor);
            e.Graphics.FillRectangle(new SolidBrush(Icon.BackColor),
                                        new Rectangle(0, 0, this.Height, this.Height));

            using (var scaledImage = ImageResize.ScaleImage(Icon, this.Width, this.Height))
            {
                var posX = (this.Height - scaledImage.Width) / 2;
                var posY = (this.Height - scaledImage.Height) / 2;

                e.Graphics.DrawImage(scaledImage, posX, posY);
            }

            e.Graphics.DrawString(Title, new Font("Arial", 12, FontStyle.Bold), new SolidBrush(this.TitleColor), 100, 10);
            e.Graphics.DrawString(Body, new Font("Arial", 8, FontStyle.Regular), new SolidBrush(this.ForeColor),
                                new RectangleF(100, 40, (this.Width - 100), this.Height));
        }

        /// <summary>
        /// Shows the notification for a set amount of time.
        /// </summary>
        /// <param name="delay">Time in milliseconds to display before dismissal.</param>
        public void Show(int delay)
        {
            base.Show();

            if (delay != 0)
            {
                _lifeTimer = new Timer
                {
                    Interval = delay
                };

                _lifeTimer.Tick += new EventHandler(Form_Close);

                this.MouseEnter += new EventHandler(Mouse_Enter);
                this.MouseLeave += new EventHandler(Mouse_Leave);

                _lifeTimer.Start();
            }
        }

        /// <summary>
        /// Returns the opposite direction
        /// </summary>
        /// <param name="direction">Input direction</param>
        /// <returns>Output direction</returns>
        private Direction GetSiblingDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                default:
                    return Direction.Left;
            }
        }

        /// <summary>
        /// Dismisses the form regardless of input or remaining time.
        /// </summary>
        /// <param name="instant">Whether to immediately close or go through the closing animation.</param>
        public void Dismiss(bool instant)
        {
            if (instant)
                base.Hide();

            base.Close();
        }

        private void Mouse_Enter(object sender, EventArgs e)
        {
            _lifeTimer.Stop();
        }

        private void Mouse_Leave(object sender, EventArgs e)
        {
            _lifeTimer.Start();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            _animator.Direction = GetSiblingDirection(_animator.Direction);
        }

        private void Form_Close(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
