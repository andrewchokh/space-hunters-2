using Godot;
using System;

/// <summary>
/// Godot Developer Log.
/// The helper static class that handles all the logic
/// related to debugging and error handling.
/// </summary>
public static class GDLog
{
    /// <summary>
    /// Checks if an [Export] field is assigned. 
    /// If null, pushes a red error and stops the node.
    /// </summary>
    public static bool IsAssigned(this Node caller, object member, string memberName)
    {
        if (member != null) return true;

        caller.CriticalError($"The exported field '{memberName}' is missing an assignment!");
        return false;
    }

    /// <summary>
    /// Pushes a standard white text message to the console. 
    /// Useful for tracking logic flow without triggering alerts.
    /// </summary>
    public static void DebugLog(this Node caller, string message)
    {
        // "Info" messages are only allowed in Debug Builds.
        if (!OS.IsDebugBuild()) return;

        GD.Print($"[DEBUG INFO] {caller.Name}: {message}");
    }

    /// <summary>
    /// Pushes a yellow warning. Use for non-critical missing polish (e.g., missing SFX).
    /// </summary>
    public static void SoftWarn(this Node caller, string message)
    {
        GD.PushWarning($"[SOFT WARNING] {caller.GetPath()}: {message}");
    }

    /// <summary>
    /// Pushes a red error and shuts down the node's processing.
    /// </summary>
    public static void CriticalError(this Node caller, string message)
    {
        GD.PushError($"[CRITICAL ERROR] {caller.GetPath()}: {message}");
        
        caller.SetProcess(false);
        caller.SetPhysicsProcess(false);
        caller.SetProcessInput(false);
    }
}