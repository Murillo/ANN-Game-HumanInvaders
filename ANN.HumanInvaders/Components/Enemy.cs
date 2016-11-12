using ANN.HumanInvaders.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN.HumanInvaders.Components
{
    enum TypeEnemy
    {
        SpaceBus,
        Spacecraft,
        Boss
    }

    class Enemy : IGameElement
    {
        public Game Game { get; private set; }
        public String Source { get; set; }
        public Boolean State { get; set; }
        public Vector2 Position { get { return position; } }
        public TypeEnemy TypeEnemy { get; set; }
        public Rectangle Retangulo { get; set; }
        public float Alpha { get; set; }
        public float Speed { get; set; }
        public Double Life { get; set; }
        Texture2D ImageEnemy;
        Vector2 position, posInicial;
        Boolean positionMin_Y = false;

        public Enemy(Game game)
        {
            Game = game;
        }

        public Enemy(Game game, Vector2 startPosition, Vector2 positionInicial, float updateSpeed)
        {
            Game = game;
            Life = 100.0;
            position = startPosition;
            Speed = updateSpeed;
            Retangulo = new Rectangle(0, 0, 12, 12);
            posInicial = positionInicial;
            Alpha = 0;
        }

        public void LoadContent()
        {

            if (TypeEnemy == TypeEnemy.SpaceBus)
            {
                ImageEnemy = Game.Content.Load<Texture2D>(@"Images\Character\Enemy\img_nave_003");
            }
            else if (TypeEnemy == TypeEnemy.Spacecraft)
            {
                ImageEnemy = Game.Content.Load<Texture2D>(@"Images\Character\Enemy\img_nave_004");
            }
            else if (TypeEnemy == TypeEnemy.Boss)
            {
                ImageEnemy = Game.Content.Load<Texture2D>(@"Images\Character\Enemy\img-boss");
            }
        }

        public void Update(GameTime gameTime)
        {
            // Set alpha effect when initialize game
            if (Alpha <= 8)
                Alpha = Alpha + 0.07f;

            position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spbatch)
        {
            spbatch.Draw(ImageEnemy, position, null, Color.White * Alpha, 0.0f, posInicial, 1.0f, SpriteEffects.None, 1.0f);
        }

        public void UnloadContent()
        { }

        public int WidthPicture()
        {
            return ImageEnemy.Width;
        }

        public int HeightPicture()
        {
            return ImageEnemy.Height;
        }

        public float PosicaoInicialY()
        {
            return posInicial.Y;
        }

        public void MoveBoss(GameTime gameTime)
        {
            if (Alpha <= 8)
                Alpha = Alpha + 0.07f;

            position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (positionMin_Y)
            {
                posInicial.Y -= ((Speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * 2);
                if ((posInicial.Y * -1) >= 380) positionMin_Y = false;
            }
            else
            {
                posInicial.Y += ((Speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * 2);
                if ((posInicial.Y * -1) < 20) positionMin_Y = true;
            }

        }

        public float Width()
        {
            throw new NotImplementedException();
        }

        public float Height()
        {
            throw new NotImplementedException();
        }
    }
}
