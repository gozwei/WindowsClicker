using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsClicker
{
    public partial class Form1 : Form
    {
        // Ładujemy bibliotekę user32.dll
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]

        // Będziemy korzystać z funkcji mouse_event z załadownej biblioteki
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        // Będziemy potrzebować kody różnych zdarzeń związnych z myszką
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public static void LeftClick(int x, int y)
        {
            // Ustawiamy kursor w punkcie x, y
            Cursor.Position = new System.Drawing.Point(x, y);
            // Symulujemy wcisnięcie lewego przycisku myszy
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            // Symulujemy puszczenie lewego przycisku myszy
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                int sleeptime = Int32.Parse(textBox1.Text);

                while (true)
                {
                    Application.DoEvents();
                    for (int i = 0; i < sleeptime; i++)
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (!checkBox1.Checked)
                        {
                            return;
                        }
                        Application.DoEvents();
                        if (!checkBox1.Checked)
                        {
                            return;
                        }
                    }

                    Application.DoEvents();
                    if (!checkBox1.Checked)
                    {
                        return;
                    }

                    int X = Cursor.Position.X;
                    int Y = Cursor.Position.Y;
                    LeftClick(X, Y);
                }
            }
        }
    }
}
