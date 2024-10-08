using UnityEngine;

public static class ColorPrint
{
    public static void Log(string message, PrintColor color)
    {
        Debug.Log($"<color={ColorCodeFor(color)}>{message}</color>");
    }

    private static string ColorCodeFor(PrintColor color)
    {
        return color switch
        {
            PrintColor.Amber => "#fcba03",
            PrintColor.Purple => "#6203fc",
            PrintColor.Emerald => "#03fcb1",
            PrintColor.Red => "#fc034a",
            _ => "#FFFFFF",
        };
    }
}

public enum PrintColor
{
    Amber,
    Purple,
    Emerald,
    Red,
}
