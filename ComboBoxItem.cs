using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VaderConsulting.Helper
{
    public class ComboBoxItem
    {
        public string _Text = "";
        public string _ID = "";

        public ComboBoxItem(string Text)
        {
            _Text = Text;
            _ID = Text;
        }

        public ComboBoxItem(string Text, string ID)
        {
            _Text = Text;
            _ID = ID;
        }

        public override string ToString()
        {
            return _Text;
        }
    }

}
