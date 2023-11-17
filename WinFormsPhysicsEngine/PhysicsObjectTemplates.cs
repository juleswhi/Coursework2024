using PhysicsEngine.Shaders;

namespace PhysicsEngine;

public static class ObjectTemplates
{
    private static ShaderDefault shaderDefault = new ShaderDefault();

    private static ShaderWall shaderWall = new ShaderWall();

    private static ShaderBall shaderBall = new ShaderBall();

    private static ShaderBallVelocity shaderBallVelocity = new ShaderBallVelocity();

    private static ShaderWater shaderWater = new ShaderWater();

    public static PhysicsObject CreateSmallBall(float originX, float originY)
    {
        return Physics.CreateStaticCircle(new Vec2(originX, originY), 5, .7F, false, shaderBallVelocity);
    }
    public static PhysicsObject CreateSmallBall_Magnet(float originX, float originY)
    {
        var oPhysicsObject = Physics.CreateStaticCircle(new Vec2(originX, originY), 5, .95F, false, shaderBallVelocity);
        Physics.ListGravityObjects.Add(oPhysicsObject);
        return oPhysicsObject;
    }

    public static PhysicsObject CreateMedBall(float originX, float originY)
    {
        return Physics.CreateStaticCircle(new Vec2(originX, originY), 10, .95F, false, shaderBallVelocity);
    }

    public static PhysicsObject CreateWater(float originX, float originY)
    {
        return Physics.CreateStaticCircle(new Vec2(originX, originY), 5, .99F, false, shaderWater);
    }

    public static PhysicsObject CreateAttractor(float originX, float originY)
    {
        var oPhysicsObject = Physics.CreateStaticCircle(new Vec2(originX, originY), 50, .95F, true, shaderBall);
        Physics.ListGravityObjects.Add(oPhysicsObject);
        return oPhysicsObject;
    }

    public static PhysicsObject CreateWall(float minX, float minY, float maxX, float maxY)
    {
        return Physics.CreateStaticBox(new Vec2(minX, minY), new Vec2(maxX, maxY), true, shaderWall, 1000000);
    }
}

