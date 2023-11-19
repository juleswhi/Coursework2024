namespace PhysicsEngine;

public class PlatformerRigidbody
{
    private Physics _physics;
    public PhysicsObject? Object { get; set; }

    public bool IsMovingLeft = false;
    public bool IsMovingRight = false;

    public bool HeldLeft = false;
    public bool HeldRight = false;

    public bool IsJumping { get; set; } = false;

    public PlatformerRigidbody(PhysicsObject physicsObject, Physics physics)
    {
        _physics = physics;
        Object = physicsObject;
    }

    public PlatformerRigidbody(Physics physics, PhysicsObject? po = null) =>
        _physics = physics;

    public void Jump()
    {
        if (IsJumping) return;
        IsJumping = true;
        _physics.ActivateAtPoint(Object.Center.ToPointF());
        _physics.AddVelocityToActive(new Vec2(Object.Velocity.X, -500f));
        _physics.ReleaseActiveObject();
    }

    public void MoveRight()
    {
        if (IsMovingRight) return;
        IsMovingRight = true;
        _physics.ActivateAtPoint(Object.Center.ToPointF());
        _physics.SetVelocityOfActive(new Vec2(200f, Object.Velocity.Y));
        _physics.ReleaseActiveObject();
    }

    public void MoveLeft()
    {
        if (IsMovingLeft) return;
        IsMovingLeft = true;
        _physics.ActivateAtPoint(Object.Center.ToPointF());
        _physics.SetVelocityOfActive(new Vec2(-200f, Object.Velocity.Y));
        _physics.ReleaseActiveObject();
    }
}
