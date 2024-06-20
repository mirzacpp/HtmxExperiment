namespace System.Text;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
    => condition ? builder.Append(value) : builder;

    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, Func<string> factory)
=> condition ? builder.Append(factory()) : builder;
}
