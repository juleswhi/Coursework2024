using static RayEngine.ShaderType;
namespace RayEngine;

public abstract class GameObject
{
    public abstract Rectangle Rectangle { get; set; }
    public abstract ShaderType Shader { get; set; }
}

public class Wall : GameObject
{
    public Wall(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = BRICK;
}

public class Door : GameObject
{
    public Door(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = DOOR;
}

public class Enemy : GameObject
{
    public Enemy(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = ENEMY;
}

