using static RayEngine.ShaderType;
namespace RayEngine;

public interface IDesigner
{
    public int MapIndex { get; set; } 
}

public abstract class GameObject : IDesigner
{
    public abstract Rectangle Rectangle { get; set; }
    public abstract ShaderType Shader { get; set; }
    public int MapIndex { get; set; }
}

public class Wall : GameObject, IDesigner
{
    public Wall(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = BRICK;
}

public class Door : GameObject, IDesigner
{
    public Door(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = DOOR;
}

public class Enemy : GameObject, IDesigner
{
    public Enemy(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = ENEMY;
}


public class Floor : GameObject, IDesigner
{
    public Floor(Rectangle _rectangle) => Rectangle = _rectangle;
    public override Rectangle Rectangle { get; set; }
    public override ShaderType Shader { get; set; } = DESIGNER_FLOOR;
}
