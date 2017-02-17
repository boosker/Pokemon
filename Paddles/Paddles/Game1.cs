using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Paddles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        clsSprite ashSprite;
        clsSprite garySprite;
        clsSprite gymSprite;
        clsSprite pokeballSprite;

        Rectangle destAsh;
        Rectangle sourceAsh;
        Rectangle destGary;
        Rectangle sourceGary;

        int frames = 0;
        float delay = 50f; // Framerate of sprite anim
        float elapsed; // Checks how many frames went by


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // changing the back buffer size changes the window size (in windowed mode)
            graphics.PreferredBackBufferWidth = 1402;
            graphics.PreferredBackBufferHeight = 700;

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
            //destAsh = new Rectangle(100, 400, 51, 235); // Starts the sprite at (100, 100) and crops the sprite sheet to 1 frame (51w, 235h)
            //destGary = new Rectangle(400, 400, 51, 235); // Starts the sprite at (400, 100) and crops the sprite sheet to 1 frame (51w, 235h)
            //destAsh = new Rectangle((int)ashSprite.position.X, (int)ashSprite.position.Y, 51, 235); // updates position of Ash
            //destGary = new Rectangle((int)garySprite.position.X, (int)garySprite.position.Y, 51, 235); // updates position of Gary

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
            // Load a 2D texture sprite
            ashSprite = new clsSprite(Content.Load<Texture2D>("paddle-ash"),
                        new Vector2(1276f, 232f),
                        new Vector2(51f, 235f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight);

            garySprite = new clsSprite(Content.Load<Texture2D>("paddle-gary"),
                        new Vector2(75f, 232f),
                        new Vector2(51f, 235f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight);

            gymSprite = new clsSprite(Content.Load<Texture2D>("gym"),
                        new Vector2(0f, 0f),
                        new Vector2(1402f, 700f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight);

            pokeballSprite = new clsSprite(Content.Load<Texture2D>("pokeball"),
                        new Vector2(683f, 332f),
                        new Vector2(36f, 36f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Free the previously allocated resources
            ashSprite.texture.Dispose();
            garySprite.texture.Dispose();
            gymSprite.texture.Dispose();
            pokeballSprite.texture.Dispose();
            spriteBatch.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        public void AnimateAsh(GameTime gameTime)// Logic that loops through Ash's animation
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceAsh = new Rectangle(51 * frames, 0, 51, 235);
        }

        public void AnimateGary(GameTime gameTime)// Logic that loops through Gary's animation
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceGary = new Rectangle(51 * frames, 0, 51, 235);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            // Change the sprite position using the keyboard
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ashSprite.position += new Vector2(0, 4);
                AnimateAsh(gameTime);
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                ashSprite.position += new Vector2(0, -4);
                AnimateAsh(gameTime);
            }
            else
            {
                sourceAsh = new Rectangle(0, 0, 51, 235); // Resets and stops anim to first frame
            }


            if (keyboardState.IsKeyDown(Keys.S))
            {
                garySprite.position += new Vector2(0, 4);
                AnimateGary(gameTime);
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                garySprite.position += new Vector2(0, -4);
                AnimateGary(gameTime);
            }
            else
            {
                sourceGary = new Rectangle(0, 0, 51, 235); // Resets and stops anim to first frame
            }



            destAsh = new Rectangle((int)ashSprite.position.X, (int)ashSprite.position.Y, 51, 235); // updates position of Ash
            destGary = new Rectangle((int)garySprite.position.X, (int)garySprite.position.Y, 51, 235); // updates position of Gary

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name = "gameTime" > Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Draw the sprite using Alpha Blend, which uses transparency information if available 
            // In 4.0, this behavior is the default; in XNA 3.1, it is not 
            spriteBatch.Begin();
            gymSprite.Draw(spriteBatch);
            spriteBatch.Draw(ashSprite.texture, destAsh, sourceAsh, Color.White);
            spriteBatch.Draw(garySprite.texture, destGary, sourceGary, Color.White);
            pokeballSprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
