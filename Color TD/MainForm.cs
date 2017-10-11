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
/*
 * A game made by Erik Mattfolk
 * 
 * Project start: Sep 24, 2017
 */
namespace Color_TD
{
    public partial class MainForm : Form
    {
        private static readonly int FPS = 60, SLEEPTIME = 1000/FPS, MAPSIZE = 480, UIWIDTH = 150;
        private static readonly float DELTATIME = 1f / FPS;
        private Bitmap canvas;
        private TDMap map;
        private Stopwatch stopWatch;
        private Player player;
        private UI ui;
        private List<Dot> enemies;
        private List<Tower> towers;
        private List<Attack> attacks;

        public MainForm ()
        {
            InitializeComponent();
            Application.Idle += GameLoop;
        }

        private void MainForm_Load (object sender, EventArgs e)
        {
            DoubleBuffered = true;
            player = new Player();
            ui = new UI();
            stopWatch = new Stopwatch();
            canvas = new Bitmap(MAPSIZE + UIWIDTH, MAPSIZE);
            enemies = new List<Dot>() { new BlackDot() };
            towers = new List<Tower>();
            attacks = new List<Attack>();
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
            Size = new Size(MAPSIZE + 16 + 150, MAPSIZE + 39);
            stopWatch.Start();
        }

        private void GameLoop (object sender, EventArgs e)
        {
            long startTime, endTime;

            while (ApplicationIsIdle())
            {
                startTime = stopWatch.ElapsedTicks;

                GameUpdate();
                Render();

                endTime = stopWatch.ElapsedTicks;
                Thread.Sleep(new TimeSpan(0, 0, 0, 0, SLEEPTIME) - new TimeSpan(endTime - startTime));
            }
        }

        private void GameUpdate ()
        {
            if (ModifierKeys == Keys.Shift)enemies.Add(new BlackDot());
            CleanupDertroyedObjects();
            UpdatePositions(DELTATIME);
            UpdateTargets();
            FireAtTargets();
            UpdateShots();
        }

        private void Render ()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.DrawImage(map.Map, 0, 0);
                foreach (Dot enemy in enemies)
                {
                    float correction = enemy.Size * enemy.Scale / 2;
                    g.DrawImage(enemy.GetImage(), enemy.Position.X - correction, enemy.Position.Y - correction, enemy.Size * enemy.Scale, enemy.Size * enemy.Scale);
                }
                foreach (Tower tower in towers)
                {
                    DrawRotated(tower, g);
                }
                foreach (Attack attack in attacks)
                {
                    if (attack.AttackType == AttackType.Laser) using (Pen p = new Pen(Color.Red)) g.DrawLine(p, attack.Shooter.Position, attack.Target.Position);
                    if (attack.AttackType == AttackType.Bolt) DrawRotated(attack, g);
                }
                g.DrawImage(ui.BackgroundImage, MAPSIZE, 0);
                g.DrawImage(ui.CoinImage, MAPSIZE + 1, 3);
                using (Brush b = new SolidBrush(Color.Black))
                {
                    using (Font f = new Font("Courier New", 16, FontStyle.Bold))
                    {
                        g.DrawString(player.Coins.ToString(), f, b, new PointF(MAPSIZE + 16, 0));
                    }
                }
                Refresh();
            }
        }

        private void CleanupDertroyedObjects ()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (!enemies[i].IsAlive) enemies.RemoveAt(i);
            }
            for (int i = attacks.Count - 1; i >= 0; i--)
            {
                if (!attacks[i].IsAlive) attacks.RemoveAt(i);
            }
        }

        private void UpdatePositions (float deltaTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                float distance = enemies[i].UpdateDistance(deltaTime);
                enemies[i].Position = map.GetPosition(distance);
            }
        }

        private void UpdateTargets ()
        {
            foreach (Tower tower in towers)
            {
                if (tower.HasTarget()) continue;
                foreach (Dot enemy in enemies)
                {
                    if (tower.DistanceTo(enemy) < tower.Range)
                    {
                        tower.Target = enemy;
                        break;
                    }
                }
            }
        }

        private void FireAtTargets ()
        {
            foreach (Tower tower in towers)
            {
                tower.UpdateFrameCount();
                Attack shot = tower.Shoot();
                if (shot != null) attacks.Add(shot);
            }
        }

        private void UpdateShots ()
        {
            foreach (Attack attack in attacks)
            {
                attack.Update(DELTATIME);
                if (attack.AttackType == AttackType.Bolt)
                {
                    foreach (Dot enemy in enemies)
                    {
                        if (attack.DistanceTo(enemy.Position) < enemy.Size * enemy.Scale + attack.Size * attack.Scale)
                        {
                            attack.ApplyDamage(enemy);
                            if (!attack.CanHit) break;
                        }
                    }
                }
            }
        }

        private void DrawRotated (GameObject gameObject, Graphics g)
        {
            float xcorrection = gameObject.Width * gameObject.Scale / 2;
            float ycorrection = gameObject.Height * gameObject.Scale / 2;
            float xTranslation = gameObject.Position.X;
            float yTranslation = gameObject.Position.Y;
            g.TranslateTransform(xTranslation, yTranslation);
            g.RotateTransform(gameObject.Rotation);
            g.TranslateTransform(-xTranslation, -yTranslation);
            g.DrawImage(gameObject.GetImage(), gameObject.Position.X - xcorrection, gameObject.Position.Y - ycorrection, gameObject.Width * gameObject.Scale, gameObject.Height * gameObject.Scale);
            g.ResetTransform();
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

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            towers.Add(new BoltTower(e.Location));
            player.Coins -= 10;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
        }
    }
}
