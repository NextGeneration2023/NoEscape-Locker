using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; //add this!!!
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace NoEscape_Locker
{
    public partial class NoEscapeForm : Form, IMessageFilter
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(
    string command,
    String returnValue,
    int returnLength,
    IntPtr winHandle);
        Rectangle BoundRect;
        Rectangle OldRect = Rectangle.Empty;
        private void EnableMouse()
        {
            Cursor.Clip = OldRect;
            Cursor.Show();
            Application.RemoveMessageFilter(this);
        }
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x201 || m.Msg == 0x202 || m.Msg == 0x203) return true;
            if (m.Msg == 0x204 || m.Msg == 0x205 || m.Msg == 0x206) return true;
            return false;
        }
        private void DisableMouse()
        {
            OldRect = Cursor.Clip;
            // Arbitrary location.
            BoundRect = new Rectangle(50, 50, 1, 1);
            Cursor.Clip = BoundRect;
            Cursor.Hide();
            Application.AddMessageFilter(this);
        }
        public void PlayMP3(string mp3file)
        {
            mciSendString($"play {mp3file} repeat", String.Empty, 0, IntPtr.Zero);
        }
        public NoEscapeForm()
        {
            Message mes = new Message();
            PreFilterMessage(ref mes);
            DisableMouse();
            PlayMP3("Bensound.mp3");
            InitializeComponent();
            Thread.Sleep(5000);
            PreFilterMessage(ref mes);
            EnableMouse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == String.Empty)
            {
                MessageBox.Show("Wrong Password");
            }
            else if(textBox1.Text == "NoEscape2023VILLEGAS")
            {
                MessageBox.Show("Password is Correct!!!");
                Environment.Exit(344);
            }
            else
            {
                MessageBox.Show("Wrong Password");
            }
        }
    }
}
