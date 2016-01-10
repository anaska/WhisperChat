using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient.Controls
{
    class ReadOnlyRichTextBox : RichTextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_VSCROLL = 0x115;
        private const int SB_PAGEBOTTOM = 7;

        
        public ReadOnlyRichTextBox()
        {
            ReadOnly = true;
            BackColor = SystemColors.Control;
            Cursor = Cursors.Arrow;         
           
            GotFocus += ReadOnlyRichTextBox_GotFocus;
            MouseDown += ReadOnlyRichTextBox_MouseDown;
            MouseUp += ReadOnlyRichTextBox_MouseDown;
            TextChanged += ReadOnlyRichTextBox_TextChanged;
        }
        public static void ScrollToBottom(RichTextBox MyRichTextBox)
        {
            SendMessage(MyRichTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            HideCaret(Handle);
        }

        protected override void OnEnter(EventArgs e)
        {
            HideCaret(Handle);
        }

        private void ReadOnlyRichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(Handle);
        }

        private void ReadOnlyRichTextBox_TextChanged(object sender, EventArgs e)
        {           
            ScrollToBottom(this);
            HideCaret(Handle);            
        }

        private delegate void AppendTextDelegate(string text, Color color, Font font);
        private delegate string GetRTFDelegate();
        private delegate void SetRTFDelegate(string str);

        private string GetRTF()
        {
            if (InvokeRequired)
            {
                GetRTFDelegate d = new GetRTFDelegate(GetRTF);
                return (string)Invoke(d);
            }
            else
                return base.Rtf;
        }

        private void SetRTF(string str)
        {
            if (InvokeRequired)
            {
                SetRTFDelegate d = new SetRTFDelegate(SetRTF);
                BeginInvoke(d, str);
            }
            else
                base.Rtf = str;
        }


        new public string Rtf
        {
            get
            {
                return GetRTF();
            }
            set
            {
                SetRTF(value);
            }
        }

        public void AppendText(string text, Color color, Font font)
        {
            if (InvokeRequired)
            {
                AppendTextDelegate d = new AppendTextDelegate(AppendText);
                BeginInvoke(d, text, color, font);
            }
            else
            {
                SelectionStart = TextLength;
                SelectionLength = 0;

                SelectionColor = color;
                SelectionFont = font;
                AppendText(text);

                SelectionFont = Font;
                SelectionColor = ForeColor;
            }
        }

        private void ReadOnlyRichTextBox_GotFocus(object sender, EventArgs e)
        {
            HideCaret(Handle);
        }
    }
}
