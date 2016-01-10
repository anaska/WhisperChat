using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient.Controls
{
    public class TextBoxWithValue : TextBox
    {     
        private string defaultValue;
        public TextBoxWithValue() :base()
        {
            InitializeComponents();
        }


        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                if (Text == "")
                    Text = value;
                defaultValue = value;
            }
        }

        public bool IsSet { get; set; } = false;
        

        private void InitializeComponents()
        {
            GotFocus += TextBoxWithValue_GotFocus;
            LostFocus += TextBoxWithValue_LostFocus;
            KeyUp += TextBoxWithValue_KeyUP;     
        }

        private void TextBoxWithValue_KeyUP(object sender, KeyEventArgs e)
        {
            if (Text == "")
            {
                IsSet = false;                
            }
            else
                IsSet = true;
        }

        private void TextBoxWithValue_LostFocus(object sender, EventArgs e)
        {
            if (IsSet)
                return;
            Text = DefaultValue;
        }

        private void TextBoxWithValue_GotFocus(object sender, EventArgs e)
        {
            if (IsSet)
                return;
            Text = "";
        }
    }
}
