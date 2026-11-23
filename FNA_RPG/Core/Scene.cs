using Microsoft.Xna.Framework;           
using Microsoft.Xna.Framework.Content;   
using Microsoft.Xna.Framework.Graphics; 

public abstract class Scene
{
	protected Texture2D Background;

	public abstract void LoadContent(ContentManager content);
	public abstract void Update(GameTime gameTime);
	public abstract void Draw(SpriteBatch spriteBatch);
}