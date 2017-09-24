using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Color_TD
{
    public partial class MainForm : Form
    {
        private Bitmap canvas, map;

        public MainForm()
        {
            InitializeComponent();
            Application.Idle += GameLoop;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            while (ApplicationIsIdle())
            {
                GameUpdate();
                Render();
                Thread.Sleep(100);
            }
        }

        private void GameUpdate()
        {
            
        }

        private void Render()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.DrawImage(map, 0, 0);
                Refresh();

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            canvas = new Bitmap(480,480);
            map = new Bitmap("..\\..\\Map1.png");
        }



        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }
        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);
        bool ApplicationIsIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, 0, 0, 0) == 0;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
        }
    }
}
