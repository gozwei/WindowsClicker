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
        // Loading user32.dll library:
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]

        // The function we are going to use: mouse_event
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        // Mouse action codes:
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

        public static void LeftClick(int x, int y)
        {
            // Setting cursor at position x, y
            Cursor.Position = new System.Drawing.Point(x, y);

            // Simulating left click down (and hold)
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);

            // Simulating left click release
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        public static void MoveMouse(int x, int y)
        {
            // Setting cursor at position x, y
            Cursor.Position = new System.Drawing.Point(x, y);
        }

        private DateTime LastChange;

        private int offsetX = 0;
        private int offsetY = 0;

        private int directionX = 1;
        private int directionY = 1;

        private int lastUserX = 0;
        private int lastUserY = 0;

        private int clicksIntrval = 100;

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;

            int lastX = Int32.Parse(textBox2.Text);
            int lastY = Int32.Parse(textBox3.Text);

            if ((X != lastX) | (Y != lastY))
            {
                LastChange = DateTime.UtcNow;
                clicksIntrval = 100;
                lastUserX = X;
                lastUserY = Y;
            }


            textBox2.Text = X.ToString();
            textBox3.Text = Y.ToString();
            textBox1.Text = clicksIntrval.ToString();

            double secondsSinceMovement = (DateTime.UtcNow - LastChange).TotalSeconds;

            textBox4.Text = secondsSinceMovement.ToString();

            if (secondsSinceMovement > 10)
            {
                if (checkMoveMouse.Checked)
                {
                    int newX;
                    int newY;

                    offsetX += 1 * directionX;
                    offsetY += 1 * directionY;

                    if (Math.Abs(offsetX) > 10)
                    {
                        directionX *= -1;
                    }

                    if (Math.Abs(offsetY) > 10)
                    {
                        directionY *= -1;
                    }

                    var directions = new List<int> {-1, 1};
                    var random = new Random();
                    if ((offsetX == 0) & (offsetY == 0))
                    {
                        directionX = directions[random.Next(directions.Count)];
                        directionY = directions[random.Next(directions.Count)];
                    }

                    newX = lastUserX + offsetX;
                    newY = lastUserY + offsetY;

                    textBox2.Text = newX.ToString();
                    textBox3.Text = newY.ToString();

                    MoveMouse(newX, newY);
                }

                if (checkLeftClicks.Checked)
                {
                    clicksIntrval -= 1;
                    if (clicksIntrval == 0)
                    {
                        clicksIntrval = 100;
                        LeftClick(X, Y);
                    }
                }
            }
        }
    }
}
