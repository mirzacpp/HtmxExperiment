namespace System;

public static class BoolExtensions
{
    public static string ToStringLowered(this bool value) => value ? "true" : "false";
}
