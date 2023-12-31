﻿using System.Runtime.InteropServices;

namespace PhysicsEngine;

internal class PreciseTimer
{
    [System.Security.SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32")]
    private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

    [System.Security.SuppressUnmanagedCodeSecurity]
    [DllImport("kernel32")]
    private static extern bool QueryPerformanceCounter(ref long PerformanceCount);

    long _ticksPerSecond = 0;
    long _previousElapsedTime = 0;

    public PreciseTimer()
    {
        QueryPerformanceFrequency(ref _ticksPerSecond);
        GetElapsedTime();
    }

    public double GetElapsedTime()
    {
        long time = 0;
        QueryPerformanceCounter(ref time);
        double elapsedTime = (double)(time - _previousElapsedTime) / (double)_ticksPerSecond;
        _previousElapsedTime = time;
        return elapsedTime;
    }
}
