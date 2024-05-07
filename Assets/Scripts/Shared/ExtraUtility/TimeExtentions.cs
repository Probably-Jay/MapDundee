using System;

public static class TimeExtentions
{
    public static void ClampAboveZero(this ref TimeSpan timeSpan)
    {
        if(timeSpan < TimeSpan.Zero)
            timeSpan = TimeSpan.Zero;
    }

    public static bool IsZero(this TimeSpan timeSpan) => timeSpan == TimeSpan.Zero;
}
