using MachineLearning.Technics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ANN.HumanInvaders
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MultilayerPerceptron mlp;
        Texture2D background, image1, image2;
        SpriteFont points;
        Vector2 vector1, vector2, vectorBackground, vectorPoints, vectorTitle;
        Rectangle space_alien, space_human;
        float speed = 4;
        float speedEnemy = 2;
        int totalPoints = 0;
        bool trainGame, startGame = false;

        public Main()
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
            Random r = new Random();

            background = Content.Load<Texture2D>("background");
            vectorBackground = new Vector2(0, 0);

            points = Content.Load<SpriteFont>(@"points");
            vectorTitle = new Vector2(10, 10);
            vectorPoints = new Vector2((GraphicsDevice.Viewport.Width - 125), 10);

            image1 = Content.Load<Texture2D>("spacecraft-alien-min");
            vector1 = new Vector2(10, 240);
            space_alien = new Rectangle(0, 0, 150, 151);

            image2 = Content.Load<Texture2D>("spacecraft-human-min");
            //vector2 = new Vector2(r.Next(400, 600), r.Next(0, 400));
            vector2 = new Vector2(600, 300);
            space_human = new Rectangle(0, 0, 150, 90);

            // Input = Position.X Player, Position.Y Player, Position.X Enemy, Position.Y Enemy
            // Output = Direction, Speed
            mlp = new MultilayerPerceptron(4, 4, 1);

            trainGame = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
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

            if (trainGame)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    vector1.Y -= speed;
                    double[,] positions = new double[,] { { vector1.X, vector1.Y, vector2.X, vector2.Y } };
                    double[] result = new double[] { 0 };
                    mlp.Training(positions, result);
                    System.Diagnostics.Debug.WriteLine(mlp.Run(new double[] { vector1.X, vector1.Y, vector2.X, vector2.Y })[0]);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    vector1.Y += speed;
                    double[,] positions = new double[,] { { vector1.X, vector1.Y, vector2.X, vector2.Y } };
                    double[] result = new double[] { 1 };
                    mlp.Training(positions, result);
                    System.Diagnostics.Debug.WriteLine(mlp.Run(new double[] { vector1.X, vector1.Y, vector2.X, vector2.Y })[0]);
                }
                else
                {
                    double[,] positions = new double[,] { { vector1.X, vector1.Y, vector2.X, vector2.Y } };
                    double[] result = new double[] { 0.5 };
                    mlp.Training(positions, result);
                    System.Diagnostics.Debug.WriteLine(mlp.Run(new double[] { vector1.X, vector1.Y, vector2.X, vector2.Y })[0]);
                }

                if (vector2.X >= 0)
                {
                    vector2.X -= speedEnemy;
                }
                else
                {
                    totalPoints += 1;
                    Random r = new Random();
                    //vector2 = new Vector2(800, r.Next(0, 400));
                    vector2 = new Vector2(600, 300);
                    trainGame = totalPoints < 1;
                    if (totalPoints >= 1)
                    {
                        trainGame = false;
                        startGame = true;
                        vector1 = new Vector2(10, 240);
                    }
                }
            }

            if (startGame)
            {
                double[] inputs = new double[] { vector1.X, vector1.Y, vector2.X, vector2.Y };
                double result = mlp.Run(inputs)[0];
                System.Diagnostics.Debug.WriteLine(result);

                if (vector2.X >= 0)
                    vector2.X -= 2;
                else
                {
                    totalPoints += 1;
                    Random r = new Random();
                    vector2 = new Vector2(r.Next(400, 600), r.Next(0, 400));
                }

                if (result >= 0 && result <= 0.4)
                    vector1.Y -= speed;
                else if (result >= 6 && result <= 1)
                    vector1.Y += speed;
            }
            
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, vectorBackground, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(image1, vector1, space_alien, Color.White);
            spriteBatch.Draw(image2, vector2, space_human, Color.White);
            spriteBatch.DrawString(points, "Human Invaders", vectorTitle, Color.White);
            spriteBatch.DrawString(points, "Points: " + totalPoints, vectorPoints, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
