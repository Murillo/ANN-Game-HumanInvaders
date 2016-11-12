using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ANN.HumanInvaders.Interface
{
    interface IGameElement
    {
        Game Game { get; }
        Boolean State { get; set; }
        Vector2 Position { get; }

        float Width();
        float Height();
        void LoadContent();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spbatch);
        void UnloadContent();
    }
}
