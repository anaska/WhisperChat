using Chat;
using ChatClient.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace ChatClient.Utils
{
    public class ChatMemory
    {

        private IDictionary<int, List<MessageContainer>> memory = new Dictionary<int, List<MessageContainer>>();
        private IDictionary rows = new Hashtable();       
        public void Add(int key, MessageContainer msc)
        {
            if (!memory.ContainsKey(key))
            {
                memory.Add(key, new List<MessageContainer>());
            }
            if (!memory[key].Contains(msc))
                memory[key].Add(msc);
        }


        public int Rows(int key)
        {
            if (!rows.Contains(key))
            {
                rows.Add(key, 30);
                return 30;
            }
            rows[key] = (int)rows[key] + 30;
            return (int)rows[key];
        }

        public void Add(int key, List<MessageContainer> msc)
        {
            if (memory.ContainsKey(key))
                memory[key].Clear();
            msc.Sort(delegate (MessageContainer x, MessageContainer y)
            {
                return x.TimeStamp.CompareTo(y.TimeStamp);
            });
            foreach (MessageContainer m in msc)
                Add(key, m);
        }

        public List<MessageContainer> this[int key]
        {
            get { return memory.ContainsKey(key) ? memory[key] : null; }
        }

        public string ToRTF(IList<MessageContainer> arr, IClient me)
        {
            ReadOnlyRichTextBox rtb = new ReadOnlyRichTextBox();
            rtb.Text = "";
            foreach (MessageContainer msc in arr)
            {
                if (msc.From == me)
                    rtb.AppendText("me", Color.Red, new Font("Arial", 14, FontStyle.Bold));
                else
                    rtb.AppendText(msc.From.Name, Color.Blue, new Font("Arial", 14, FontStyle.Bold));
                rtb.AppendText(string.Format("({0})", msc.TimeStamp.ToString("HH:mm")));
                rtb.AppendText(":" + msc.Message + Environment.NewLine);
            }

            return rtb.Rtf;
        }

    }
}
