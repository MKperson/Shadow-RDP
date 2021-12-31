using System.Drawing;
using System.Windows.Forms;

namespace Shadow_RDP
{
    // ReSharper disable once InconsistentNaming
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        public string DisplayText
        {
            get => lblDisplay.Text;
            set
            {
                lblDisplay.Text = value;
                ChangeSize();
            }

        }


        private void ChangeSize()
        {

            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(lblDisplay.Text, lblDisplay.Font);

                Size newSize = new Size((int)size.Width, (int)size.Height);
                MaximumSize = newSize;
                MinimumSize = newSize;

            }

            lblDisplay.Left = (Width / 2) - (lblDisplay.Width / 2);
            lblDisplay.Top = (Height / 2) - (lblDisplay.Height / 2);


        }
    }

}
