using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace VaderConsulting.Helper
{
    public partial class frmProgress : Form
    {
        private bool _LoadComplete = false;
        
        public frmProgress()
        {
            InitializeComponent();
        }

        [STAThreadAttribute] 
        public void AddText(string Text)
        {
            // http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c

            this.Invoke(new Action(() => txtProgress.AppendText(Text)));
        }

        private void frmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }

            VaderConsulting.Helper.Properties.ProgressWindowState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                VaderConsulting.Helper.Properties.frmProgressLocation = this.Location;
                VaderConsulting.Helper.Properties.frmProgressSize = this.Size;
            }
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            //if (this.Location.X < 0)
            //{
            //    this.Location = new Point(10, 10);
            //}

            _LoadComplete = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProgress.Clear();
        }

        private void btnSetMark_Click(object sender, EventArgs e)
        {
            txtProgress.AppendText("======= " + DateTime.Now.ToString() + " ================================================================\n");
        }

        private void frmProgress_Shown(object sender, EventArgs e)
        {
            if (VaderConsulting.Helper.Properties.frmProgressLocation.X != 0 && VaderConsulting.Helper.Properties.frmProgressLocation.Y != 0)
            {
                this.Location = VaderConsulting.Helper.Properties.frmProgressLocation;
            }

            if (VaderConsulting.Helper.Properties.frmProgressSize.Width != 0 && VaderConsulting.Helper.Properties.frmProgressSize.Height != 0)
            {
                this.Size = VaderConsulting.Helper.Properties.frmProgressSize;
            }

            if (!VaderConsulting.Helper.Methods.IsOnScreen(this))
            {
                this.Location = new Point(10, 10);
            }
        }

        private void frmProgress_ResizeEnd(object sender, EventArgs e)
        {
            if (_LoadComplete)
            {
                VaderConsulting.Helper.Properties.frmProgressSize = this.Size;
                VaderConsulting.Helper.Properties.ProgressWindowState = this.WindowState;
            }
        }

        private void frmProgress_Move(object sender, EventArgs e)
        {
            if (_LoadComplete)
            {
                VaderConsulting.Helper.Properties.frmProgressLocation = this.Location;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtProgress.SelectedText != "")
            {
                Clipboard.SetText(txtProgress.SelectedText);
            }
            else
            {
                Clipboard.SetText(txtProgress.Text);
            }
            txtProgress.SelectionLength = 0;
            txtProgress.SelectionStart = txtProgress.TextLength;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            txtProgress.Focus();
            txtProgress.SelectAll();
        }

    }
}
