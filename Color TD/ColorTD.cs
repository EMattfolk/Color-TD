using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Color_TD
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ColorTD : Game
    {
        private static readonly int MAPSIZE = 480, UIWIDTH = 150;
        private TDMap map;
        private Player player;
        private UI ui;
        private Tower heldTower, clickedTower;
        private List<Dot> enemies;
        private List<Tower> towers;
        private List<Attack> attacks;
        private List<Texture2D> enemySprites, towerSprites, boltSprites, uiSprites, mapSprites;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ColorTD()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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
            uiSprites = new List<Texture2D>();
            mapSprites = new List<Texture2D>();
            map = new TDMap(0, new Vector2[] {
                new Vector2(490,69),
                new Vector2(67,69),
                new Vector2(67,175),
                new Vector2(395,175),
                new Vector2(395,280),
                new Vector2(63,280),
                new Vector2(63,417),
                new Vector2(490,417)
            });
            graphics.PreferredBackBufferWidth = MAPSIZE + UIWIDTH;
            graphics.PreferredBackBufferHeight = MAPSIZE;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //enemies.Add(new BlackDot());
            CleanupDertroyedObjects();
            UpdatePositions(gameTime);
            UpdateTargets();
            FireAtTargets(gameTime);
            UpdateShots(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
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

        private void UpdatePositions(GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].UpdateDistance(gameTime);
                enemies[i].Position = map.GetPosition(enemies[i].Distance);
            }
            if (heldTower != null)
            {
                heldTower.Position = Mouse.GetState().Position.ToVector2();
                heldTower.HasValidPosition = heldTower.Position.X < MAPSIZE;
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
