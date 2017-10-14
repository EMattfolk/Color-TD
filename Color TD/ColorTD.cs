using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Color_TD
{
    public class ColorTD : Game
    {
        private static readonly int MAPSIZE = 480, UIWIDTH = 150, PATHWIDTH = 20;
        private TDMap map;
        private Player player;
        private UI ui;
        private Tower heldTower, clickedTower;
        private List<Dot> enemies;
        private List<Tower> towers;
        private List<Attack> attacks;
        private List<Texture2D> enemySprites, towerSprites, boltSprites, laserSprites, uiSprites, mapSprites, circleSprites;
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
            enemies = new List<Dot>() { new BlackDot() };
            towers = new List<Tower>();
            attacks = new List<Attack>();
            enemySprites = new List<Texture2D>();
            towerSprites = new List<Texture2D>();
            boltSprites = new List<Texture2D>();
            laserSprites = new List<Texture2D>();
            uiSprites = new List<Texture2D>();
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

            enemySprites.Add(Content.Load<Texture2D>("Graphics\\Black_dot"));
            towerSprites.Add(Content.Load<Texture2D>("Graphics\\Tower_laser"));
            towerSprites.Add(Content.Load<Texture2D>("Graphics\\Tower_bolt"));
            boltSprites.Add(Content.Load<Texture2D>("Graphics\\Bolt_blue"));
            laserSprites.Add(Content.Load<Texture2D>("Graphics\\Laser"));
            uiSprites.Add(Content.Load<Texture2D>("Graphics\\Button_Bolt"));
            uiSprites.Add(Content.Load<Texture2D>("Graphics\\Coin"));
            uiSprites.Add(Content.Load<Texture2D>("Graphics\\Heart"));
            mapSprites.Add(Content.Load<Texture2D>("Graphics\\UI_Background"));
            mapSprites.Add(Content.Load<Texture2D>("Graphics\\Map1"));
            circleSprites.Add(Content.Load<Texture2D>("Graphics\\Circle_red"));
            circleSprites.Add(Content.Load<Texture2D>("Graphics\\Circle_green"));
            font = Content.Load<SpriteFont>("Fonts\\Courier New");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) enemies.Add(new BlackDot());
            CheckForMouseInput();
            CleanupDertroyedObjects();
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
                spriteBatch.Draw(enemySprites[enemy.GetSpriteIndex()], enemy.Position - correction, null, Color.White, 0, Vector2.Zero, enemy.Scale, SpriteEffects.None, 0);
            }
            foreach (Tower tower in towers)
            {
                Vector2 correction = new Vector2(tower.Size * tower.Scale);
                spriteBatch.Draw(towerSprites[tower.GetSpriteIndex()], tower.Position, null, Color.White, tower.Rotation, correction, tower.Scale, SpriteEffects.None, 0);
            }
            foreach (Attack attack in attacks)
            {
                if (attack.AttackType == AttackType.Laser)
                {
                    spriteBatch.Draw(laserSprites[0],
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
                    spriteBatch.Draw(boltSprites[attack.GetSpriteIndex()], attack.Position, null, Color.White, attack.Rotation, correction, attack.Scale, SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(mapSprites[0], new Vector2(MAPSIZE, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            foreach (UIElement element in ui.UIElements)
            {
                if (element.SpriteIndex != -1)
                {
                    spriteBatch.Draw(uiSprites[element.SpriteIndex], element.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                else if (element.Text != "")
                {
                    string text;
                    if (element.Text == "PLAYERCOINS") text = player.Coins.ToString();
                    else if (element.Text == "PLAYERLIFE") text = player.Lives.ToString();
                    else text = element.Text;
                    spriteBatch.DrawString(font, text, element.Position, Color.Black);
                }
            }
            if (heldTower != null)
            {
                Vector2 correction = new Vector2(heldTower.Size * heldTower.Scale);
                spriteBatch.Draw(towerSprites[heldTower.GetSpriteIndex()], heldTower.Position, null, Color.White, heldTower.Rotation, correction, heldTower.Scale, SpriteEffects.None, 0);
                spriteBatch.Draw(circleSprites[heldTower.HasValidPosition ? 1 : 0], heldTower.Position - new Vector2(heldTower.Range), null, Color.White, 0, Vector2.Zero, heldTower.Range/100f, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckForMouseInput ()
        {
            currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (heldTower != null && heldTower.HasValidPosition)
                {
                    player.Coins -= heldTower.Cost;
                    towers.Add(Tower.FromTowerType(heldTower.TowerType));
                    towers[towers.Count - 1].Position = currentMouseState.Position.ToVector2();
                    if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift) || player.Coins < heldTower.Cost) heldTower = null;
                }
                UIElement clickedElement = ui.GetElementAt(currentMouseState.Position.ToVector2());
                if (clickedElement != null)
                {
                    heldTower = Tower.FromTowerType(clickedElement.HeldTowerType);
                    if (player.Coins < heldTower.Cost) heldTower = null;
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

        private void UpdatePositions (GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].UpdateDistance(gameTime);
                enemies[i].Position = map.GetPosition(enemies[i].Distance);
            }
            if (heldTower != null)
            {
                heldTower.Position = Mouse.GetState().Position.ToVector2();
                heldTower.HasValidPosition = heldTower.Position.X < MAPSIZE && map.DistanceToPath(heldTower.Position) > PATHWIDTH;
                foreach (Tower tower in towers)
                {
                    if (heldTower.DistanceTo(tower) < heldTower.Scale * heldTower.Size / 2)
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
    }
}
