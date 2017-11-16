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
        private static readonly int MAPSIZE = 480, UIWIDTH = 150, PATHWIDTH = 30;
        private TDMap map;
        private Player player;
        private UI ui;
        private Tower heldTower, clickedTower;
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
                new Vector2(490,65),
                new Vector2(65,65),
                new Vector2(65,185),
                new Vector2(395,185),
                new Vector2(395,285),
                new Vector2(65,285),
                new Vector2(65,415),
                new Vector2(490,415)
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
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt1"));
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt2"));
            BoltTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt3"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser1"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser2"));
            LaserTower.Sprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser3"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt1"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt2"));
            BoltAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Bolt3"));
            LaserAttack.Sprites.Add(Content.Load<Texture2D>("Graphics\\Laser1"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Coin"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Heart"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Laser"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Bolt"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Start"));
            UIElement.Sprites.Add(Content.Load<Texture2D>("Graphics\\Button_Upgrade"));
            UIElement.Fonts.Add(Content.Load<SpriteFont>("Fonts\\Courier New16"));
            UIElement.Fonts.Add(Content.Load<SpriteFont>("Fonts\\Courier New12"));
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

            CheckForMouseInput();
            CleanupDertroyedObjects();
            SpawnEnemies(gameTime);
            UpdatePositions(gameTime);
            UpdateTargets();
            FireAtTargets(gameTime);
            UpdateShots(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(mapSprites[map.SpriteIndex],Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
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
                    spriteBatch.Draw(attack.GetSprite(),
                        new Rectangle((int)attack.Shooter.Position.X, (int)attack.Shooter.Position.Y, (int)attack.Shooter.DistanceTo(attack.Target), 1),
                        null,
                        Color.White,
                        (float)Math.Atan2(attack.Target.Position.Y - attack.Shooter.Position.Y, attack.Target.Position.X - attack.Shooter.Position.X),
                        Vector2.Zero,
                        SpriteEffects.None,
                        0);
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
                    if (element.Text == "PLAYERCOINS") text = player.Coins.ToString();
                    else if (element.Text == "PLAYERLIFE") text = player.Lives.ToString();
                    else if (element.Text == "TOWERINFO") text = clickedTower.GetInfo();
                    else if (element.Text == "TOWERUPGRADECOST") text = clickedTower.UpgradeCost.ToString();
                    else text = element.Text;
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
                spriteBatch.Draw(clickedTower.GetSprite(), new Vector2(MAPSIZE + UIWIDTH / 2 - 32, MAPSIZE / 2 - 32), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                DrawCircleAroundTower(clickedTower, true);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckForMouseInput ()
        {
            currentMouseState = Mouse.GetState();

            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                heldTower = null;
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                UIElement clickedElement = ui.GetElementAt(currentMouseState.Position.ToVector2());
                if (clickedElement == null) clickedTower = ClickedTower(currentMouseState.Position.ToVector2());

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
                    else
                    {
                        heldTower = Tower.FromTowerType(clickedElement.HeldTowerType);
                        if (player.Coins < heldTower.Cost) heldTower = null;
                    }
                }
                else if (heldTower == null && clickedTower != null)
                {
                    ui.SetLayout("towerinfo");
                }
                else
                {
                    ui.SetLayout("standard");
                    clickedTower = null;
                }
            }

            previousMouseState = currentMouseState;
        }

        private void CleanupDertroyedObjects ()
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

        private void SpawnEnemies (GameTime gameTime)
        {
            if (spawner.IsIdle) return;
            spawner.Update(gameTime);
            enemies.AddRange(spawner.QueuedEnemies);
            spawner.QueuedEnemies.Clear();
        }

        private void UpdatePositions (GameTime gameTime)
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
                        if (attack.DistanceTo(enemy) < enemy.Size * enemy.Scale + attack.Size * attack.Scale)
                        {
                            attack.ApplyDamage(enemy);
                            if (!attack.CanHit) break;
                        }
                    }
                }
            }
        }

        private void DrawCircleAroundTower (Tower tower, bool isValid)
        {
            spriteBatch.Draw(circleSprites[isValid ? 1 : 0], tower.Position - new Vector2(tower.Range), null, Color.White, 0, Vector2.Zero, tower.Range / 100f, SpriteEffects.None, 0);
        }

        private Tower ClickedTower (Vector2 mousePosition)
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
    }
}
