using Godot;
using System;

public static class Helper
{
    /// <summary>Gets system time in seconds</summary>
    public static float GetTime() => OS.GetTicksMsec() / 1000.0f;

    /// <summary>Returns true if action has cooled down</summary>
    public static bool IsCooledDown(float lastExecuted, float cooldownRate) => GetTime() - lastExecuted >= cooldownRate;

    /// <summary>Returns <see cref="Vector2"/> delta of last position and current position</summary>
    public static Vector2 DeltaOfVectors(Vector2 lastPos, Vector2 curPos)
    {
        return new Vector2(curPos.x - lastPos.x, curPos.y - lastPos.y);
    }
}