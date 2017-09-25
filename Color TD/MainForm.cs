using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private static readonly int FPS = 60, SLEEPTIME = 1000/FPS;
        private double lastUpdateTime;
        private Bitmap canvas;
        private TDMap map;
        private Stopwatch stopWatch;
        private List<Dot> enemies;

        public MainForm()
        {
            InitializeComponent();
            Application.Idle += GameLoop;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            stopWatch = new Stopwatch();
            canvas = new Bitmap(480, 480);
            enemies = new List<Dot>() { new Dot(100, 32, 0) };
            map = new TDMap("..\\..\\Map1.png", new Point[] {
                new Point(490,69),
                new Point(67,69),
                new Point(67,175),
                new Point(395,175),
                new Point(395,280),
                new Point(63,280),
                new Point(63,417),
                new Point(490,417)
            });
            stopWatch.Start();
            lastUpdateTime = stopWatch.Elapsed.TotalSeconds;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            long startTime, endTime;

            while (ApplicationIsIdle())
            {
                startTime = stopWatch.ElapsedTicks;

                GameUpdate();
                Render();

                endTime = stopWatch.ElapsedTicks;
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, SLEEPTIME) - new TimeSpan(endTime - startTime)); //TODO: add if statement
            }
        }

        private void GameUpdate()
        {
            double deltaTime = stopWatch.Elapsed.TotalSeconds - lastUpdateTime;
            lastUpdateTime = stopWatch.Elapsed.TotalSeconds;
            UpdatePositions(deltaTime);
        }

        private void Render()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.DrawImage(map.Map, 0, 0);
                foreach (Dot enemy in enemies)
                {
                    float correction = enemy.Size / 2f;
                    g.DrawImage(Dot.Image, (float)(enemy.Position.X - correction), (float)(enemy.Position.Y - correction), enemy.Size, enemy.Size);
                }
                Refresh();
            }
        }

        private void UpdatePositions(double deltaTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                double distance = enemies[i].UpdateDistance(deltaTime);
                enemies[i].Position = map.GetPosition(distance);
            }
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
