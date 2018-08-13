using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;

using Notification.Enum;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var colorNames = typeof(Color)
                .GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Select(p => p.Name)
                .ToArray();

            foreach (var cb in new[] { cbBackgroundColor, cbTitleColor, cbBodyColor, cbIconBackColor })
                cb.Items.AddRange(colorNames);

            cbStyle.DataSource = Enum.GetValues(typeof(Style));
            cbDirection.DataSource = Enum.GetValues(typeof(Direction));
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var notification = new Notification.Control.Notification((Style)cbStyle.SelectedItem, 
                (int)nudDuration.Value, (Direction)cbDirection.SelectedItem)
            {
                Title = txtTitle.Text,
                Body = txtBody.Text,

                TitleColor = Color.FromName(cbTitleColor.SelectedItem.ToString()),
                ForeColor = Color.FromName(cbBodyColor.SelectedItem.ToString()),
                BackColor = Color.FromName(cbBackgroundColor.SelectedItem.ToString()),

                ScreenPadding = 10,

                Icon = new Notification.Model.Icon()
                {
                    Image = Image.FromFile(txtImgPath.Text),
                    Padding = (int)nudIconPadding.Value,
                    BackColor = Color.FromName(cbIconBackColor.SelectedItem.ToString())
                }
            };

            notification.Show();
        }

        private void txtImgPath_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Title = "Notification Test Form | Import Image",
                Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png"
            })
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtImgPath.Text = ofd.FileName;
        }
    }
}
