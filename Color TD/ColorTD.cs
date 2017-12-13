using Color_TD.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Color_TD
{
    public class ColorTD : Game
    {
        private static readonly int MAPSIZE = 480, UIWIDTH = 150, PATHWIDTH = 40;
        private TDMap map;
        private Player player;
        private UI ui;
        private Tower heldTower, clickedTower;
        private Dot clickedEnemy;
        private WaveSpawner spawner;
        private List<Dot> enemies;
        private List<Tower> towers;
        private List<Attack> attacks;
        private List<Texture2D> mapSprites, circleSprites;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState currentMouseState, previousMouseState;
        SpriteFont font;

        public ColorTD()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            heldTower = null;
            clickedTower = null;
            clickedEnemy = null;
            player = new Player();
            ui = new UI(MAPSIZE);
            spawner = new WaveSpawner();
            enemies = new List<Dot>();
            towers = new List<Tower>();
            attacks = new List<Attack>();
            mapSprites = new List<Texture2D>();
            circleSprites = new List<Texture2D>();
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;
            map = new TDMap(1, new Vector2[] {
                new Vector2(505, 7),
                new Vector2(448, 28),
                new Vector2(375, 48),
                new Vector2(327, 69),
                new Vector2(314, 83),
                new Vector2(326, 103),
                new Vector2(359, 132),
                new Vector2(380, 150),
                new Vector2(378, 160),
                new Vector2(357, 168),
                new Vector2(287, 176),
                new Vector2(141, 180),
                new Vector2(85, 189),
                new Vector2(62, 209),
                new Vector2(70, 245),
                new Vector2(110, 279),
                new Vector2(153, 297),
                new Vector2(209, 299),
                new Vector2(261, 290),
                new Vector2(382, 258),
                new Vector2(418, 259),
                new Vector2(434, 288),
                new Vector2(427, 333),
                new Vector2(401, 369),
                new Vector2(369, 396),
                new Vector2(320, 408),
                new Vector2(256, 406),
                new Vector2(103, 372),
                new Vector2(49, 361),
                new Vector2(25, 371),
                new Vector2(45, 391),
                new Vector2(273, 477),
                new Vector2(325, 497)
            });
            graphics.PreferredBackBufferWidth = MAPSIZE + UIWIDTH;
            graphics.PreferredBackBufferHeight = MAPSIZE;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Black"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Blue"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Purple"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Green"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Red"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Yellow"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Cyan"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_White"));
            Dot.Sprites.Add(Content.Load<Texture2D>("Graphics\\Dot_Rainbow"));
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt1"));
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt2"));
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt3"));
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt4"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser1"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser2"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser3"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser4"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt1"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt2"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt3"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt4"));
            LaserAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Laser1"));
            LaserAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Laser2"));
            LaserAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Laser3"));
            LaserAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Laser4"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Coin"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Heart"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Laser"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Bolt"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Start"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Upgrade"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Sell"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Game_Over"));
            UIElement.Fonts.Add(Content.Load<SpriteFont>("Fonts\\Courier New16"));
            UIElement.Fonts.Add(Content.Load<SpriteFont>("Fonts\\Courier New12"));
            UIElement.Fonts.Add(Content.Load<SpriteFont>("Fonts\\Calibri16"));
            mapSprites.Add(Content.Load<Texture2D>("Graphics\\UI_Background"));
            mapSprites.Add(Content.Load<Texture2D>("Graphics\\Map1"));
            circleSprites.Add(Content.Load<Texture2D>("Graphics\\Circle_red"));
            circleSprites.Add(Content.Load<Texture2D>("Graphics\\Circle_green"));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!IsGameOver())
            {
                CheckForMouseInput();
                CleanupDertroyedObjects();
                SpawnEnemies(gameTime);
                UpdatePositions(gameTime);
                UpdateTargets();
                FireAtTargets(gameTime);
                UpdateShots(gameTime);
            }
            else
            {
                CleanupDertroyedObjects();
                UpdatePositions(gameTime);
                UpdateShots(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(mapSprites[map.SpriteIndex], Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            foreach (Dot enemy in enemies)
            {
                Vector2 correction = new Vector2(enemy.Size * enemy.Scale / 2);
                spriteBatch.Draw(enemy.GetSprite(), enemy.Position - correction, null, Color.White, 0, Vector2.Zero, enemy.Scale, SpriteEffects.None, 0);
            }
            foreach (Tower tower in towers)
            {
                Vector2 correction = new Vector2(tower.Size * tower.Scale);
                spriteBatch.Draw(tower.GetSprite(), tower.Position, null, Color.White, tower.Rotation, correction, tower.Scale, SpriteEffects.None, 0);
            }
            foreach (Attack attack in attacks)
            {
                if (attack.AttackType == AttackType.Laser)
                {
                    LaserAttack a = (LaserAttack)attack;
                    float rotation = (float)Math.Atan2(attack.Target.Position.Y - attack.Shooter.Position.Y, attack.Target.Position.X - attack.Shooter.Position.X);

                    spriteBatch.Draw(attack.GetSprite(),
                        new Rectangle((int)(attack.Shooter.Position.X + Math.Sin(rotation)), (int)(attack.Shooter.Position.Y + Math.Cos(rotation)), (int)attack.Shooter.DistanceTo(attack.Target), 2),
                        null,
                        Color.White,
                        rotation,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);

                    Vector2 correction = new Vector2(-attack.Shooter.Size * attack.Shooter.Scale / 2, a.GetSplashSprite().Width / 2);
                    spriteBatch.Draw(a.GetSplashSprite(), attack.Shooter.Position, null, Color.White, attack.Shooter.Rotation, correction, 1, SpriteEffects.None, 0);
                }
                if (attack.AttackType == AttackType.Bolt)
                {
                    Vector2 correction = new Vector2(attack.Width * attack.Scale, attack.Height * attack.Scale);
                    spriteBatch.Draw(attack.GetSprite(), attack.Position, null, Color.White, attack.Rotation, correction, attack.Scale, SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(mapSprites[0], new Vector2(MAPSIZE, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            foreach (UIElement element in ui.UIElements)
            {
                if (element.Type == UIElementType.Image)
                {
                    spriteBatch.Draw(element.GetSprite(), element.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                else if (element.Type == UIElementType.Text)
                {
                    string text;
                    switch (element.Text)
                    {
                        case "PLAYERCOINS":
                            text = player.Coins.ToString();
                            break;
                        case "PLAYERLIFE":
                            text = player.Lives.ToString();
                            break;
                        case "TOWERINFO":
                            text = clickedTower.GetInfo();
                            break;
                        case "TOWERUPGRADECOST":
                            text = clickedTower.UpgradeCost.ToString();
                            break;
                        case "ENEMYINFO":
                            text = clickedEnemy.GetInfo();
                            break;
                        case "TOWERSELLVALUE":
                            text = clickedTower.SellValue.ToString();
                            break;
                        default:
                            text = element.Text;
                            break;
                    }
                    spriteBatch.DrawString(element.Font, text, element.Position, Color.Black);
                }
            }
            if (heldTower != null)
            {
                Vector2 correction = new Vector2(heldTower.Size * heldTower.Scale);
                spriteBatch.Draw(heldTower.GetSprite(), heldTower.Position, null, Color.White, heldTower.Rotation, correction, heldTower.Scale, SpriteEffects.None, 0);
                DrawCircleAroundTower(heldTower, heldTower.HasValidPosition);
            }
            if (clickedTower != null)
            {
                spriteBatch.Draw(clickedTower.GetSprite(), new Vector2(MAPSIZE + UIWIDTH / 2 - 32, MAPSIZE / 3), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                DrawCircleAroundTower(clickedTower, true);
            }
            if (clickedEnemy != null)
            {
                spriteBatch.Draw(clickedEnemy.GetSprite(), new Vector2(MAPSIZE + UIWIDTH / 2 - 32, MAPSIZE / 2 - 32), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if (IsGameOver())
            {
                spriteBatch.Draw(UIElement.Sprites[UI.GameOver], new Vector2(160, 200), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool IsGameOver()
        {
            if (player.Lives <= 0)
            {
                player.Lives = 0;
                return true;
            }
            return spawner.IsEmpty && spawner.IsIdle && enemies.Count == 0;
        }

        private void CheckForMouseInput()
        {
            currentMouseState = Mouse.GetState();

            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                heldTower = null;
                clickedTower = null;
                clickedEnemy = null;
                ui.SetLayout("standard");
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                UIElement clickedElement = ui.GetElementAt(currentMouseState.Position.ToVector2());
                if (clickedElement == null)
                {
                    clickedEnemy = ClickedEnemy(currentMouseState.Position.ToVector2());
                    clickedTower = ClickedTower(currentMouseState.Position.ToVector2());
                }

                if (heldTower != null && heldTower.HasValidPosition)
                {
                    player.Coins -= heldTower.Cost;
                    towers.Add(Tower.FromTowerType(heldTower.TowerType));
                    towers[towers.Count - 1].Position = currentMouseState.Position.ToVector2();
                    if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift) || player.Coins < heldTower.Cost) heldTower = null;
                }
                else if (clickedElement != null)
                {
                    if (clickedElement.SpriteIndex == UI.StartButton)
                    {
                        spawner.SpawnNextWave();
                    }
                    else if (clickedElement.SpriteIndex == UI.UpgradeButton)
                    {
                        if (clickedTower.CanUpgrade && clickedTower.UpgradeCost <= player.Coins)
                        {
                            player.Coins -= clickedTower.UpgradeCost;
                            clickedTower.Upgrade();
                        }
                    }
                    else if (clickedElement.SpriteIndex == UI.SellButton)
                    {
                        player.Coins += clickedTower.SellValue;
                        towers.Remove(clickedTower);
                        clickedTower = null;
                        ui.SetLayout("standard");
                    }
                    else
                    {
                        heldTower = Tower.FromTowerType(clickedElement.HeldTowerType);
                        if (player.Coins < heldTower.Cost) heldTower = null;
                    }
                }
                else if (heldTower == null && clickedEnemy != null)
                {
                    ui.SetLayout("enemyinfo");
                }
                else if (heldTower == null && clickedTower != null)
                {
                    ui.SetLayout("towerinfo");
                }
                else
                {
                    ui.SetLayout("standard");
                    clickedTower = null;
                    clickedEnemy = null;
                }
            }

            previousMouseState = currentMouseState;
        }

        private void CleanupDertroyedObjects()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (map.HasFinished(enemies[i].Distance))
                {
                    player.Lives -= enemies[i].Worth;
                    enemies[i].Kill();
                }
                if (!enemies[i].IsAlive)
                {
                    player.Coins += enemies[i].Worth;
                    enemies.RemoveAt(i);
                }
            }
            for (int i = attacks.Count - 1; i >= 0; i--)
            {
                if (!attacks[i].IsAlive) attacks.RemoveAt(i);
            }
        }

        private void SpawnEnemies(GameTime gameTime)
        {
            if (spawner.IsIdle) return;
            spawner.Update(gameTime);
            enemies.AddRange(spawner.QueuedEnemies);
            spawner.QueuedEnemies.Clear();
        }

        private void UpdatePositions(GameTime gameTime) // TODO: fixa ordning
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].UpdateDistance(gameTime);
                enemies[i].MoveTo(map.GetPosition(enemies[i].Distance), gameTime);
            }
            if (heldTower != null)
            {
                heldTower.Position = Mouse.GetState().Position.ToVector2();
                heldTower.HasValidPosition = heldTower.Position.X < MAPSIZE && map.DistanceToPath(heldTower.Position) > PATHWIDTH * .8f;
                foreach (Tower tower in towers)
                {
                    if (heldTower.DistanceTo(tower) < heldTower.Scale * heldTower.Size * .8f)
                    {
                        heldTower.HasValidPosition = false;
                        break;
                    }
                }
            }
        }

        private void UpdateTargets()
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

        private void FireAtTargets(GameTime gameTime)
        {
            foreach (Tower tower in towers)
            {
                tower.Update(gameTime);
                Attack shot = tower.Shoot();
                if (shot != null) attacks.Add(shot);
            }
        }

        private void UpdateShots(GameTime gameTime)
        {
            foreach (Attack attack in attacks)
            {
                attack.Update(gameTime);
                if (attack.AttackType == AttackType.Bolt)
                {
                    foreach (Dot enemy in enemies)
                    {
                        if (attack.DistanceTo(enemy) < (enemy.Size * enemy.Scale + attack.Size * attack.Scale) / 2)
                        {
                            attack.ApplyDamage(enemy);
                            if (!attack.CanHit) break;
                        }
                    }
                }
            }
        }

        private void DrawCircleAroundTower(Tower tower, bool isValid)
        {
            spriteBatch.Draw(circleSprites[isValid ? 1 : 0], tower.Position - new Vector2(tower.Range), null, Color.White, 0, Vector2.Zero, tower.Range / 100f, SpriteEffects.None, 0);
        }

        private Tower ClickedTower(Vector2 mousePosition)
        {
            foreach (Tower tower in towers)
            {
                if (tower.DistanceTo(mousePosition) < tower.Size * tower.Scale / 2)
                {
                    return tower;
                }
            }
            return null;
        }

        private Dot ClickedEnemy(Vector2 mousePosition)
        {
            foreach (Dot enemy in enemies)
            {
                if (enemy.DistanceTo(mousePosition) < enemy.Size * enemy.Scale / 2 + 5)
                {
                    return enemy;
                }
            }
            return null;
        }
    }
}
