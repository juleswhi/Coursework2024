﻿namespace PhysicsEngine;

public class FastLoop
{
    PreciseTimer _timer = new PreciseTimer();
    public delegate void LoopCallback(double elapsedTime);
    LoopCallback _callback;
    public FastLoop(LoopCallback callback)
    {
        _callback = callback;
        Application.Idle += OnApplicationEnterIdle;
    }

    private void OnApplicationEnterIdle(object? sender, EventArgs e)
    {
        while(CInterop.IsApplicationIdle())
        {
            _callback(_timer.GetElapsedTime());
        }
    }
}
