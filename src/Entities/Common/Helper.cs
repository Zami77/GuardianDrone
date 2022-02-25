using Godot;
using System;

public static class Helper
{
    /// <summary>Gets system time in seconds</summary>
    public static float GetTime() => OS.GetTicksMsec() / 1000.0f;

    /// <summary>Returns true if action has cooled down</summary>
    public static bool IsCooledDown(float lastExecuted, float cooldownRate) => GetTime() - lastExecuted >= cooldownRate;
}