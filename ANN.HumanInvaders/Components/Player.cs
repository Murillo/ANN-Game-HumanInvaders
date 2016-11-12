using ANN.HumanInvaders.Interface;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANN.HumanInvaders.Components
{
    class Player : IGameElement
    {
        public Game Game { get; private set; }
        public Boolean State { get; set; }
        public Rectangle RectangleImage { get; set; }
        public Rectangle RectanglePosition { get; set; }
        public Vector2 Position { get { return _positionImage; } }
        public float Alpha { get; set; }
        public Point FrameSize { private get; set; }
        public Point CurrentFrame { private get; set; }
        public Point SheetSize { private get; set; }
        private Vector2 _positionImage;
        private Texture2D _images;

        public void Draw(SpriteBatch spbatch)
        {
            throw new NotImplementedException();
        }

        public float Height()
        {
            throw new NotImplementedException();
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public float Width()
        {
            throw new NotImplementedException();
        }
    }
}
