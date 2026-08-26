using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VaderConsulting.Helper
{
    public class CustomToolTip : ToolTip
    {
        Bitmap _Image = null;

        public CustomToolTip(Bitmap Image)
        {
            this.OwnerDraw = true;
            this.Popup += new PopupEventHandler(this.OnPopup);
            this.Draw += new DrawToolTipEventHandler(this.OnDraw);
            
            _Image = Image;
        }

        private void OnPopup(object sender, PopupEventArgs e) // use this event to set the size of the tool tip
        {
            e.ToolTipSize = _Image.Size; // 600, 1000);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) // use this to customzie the tool tip
        {
            Graphics g = e.Graphics;

            // to set the tag for each button or object
            //Control parent = e.AssociatedControl;

            //create your own custom brush to fill the background with the image
            TextureBrush b = new TextureBrush(_Image);

            g.FillRectangle(b, e.Bounds);
            b.Dispose();
        }
    }
}
