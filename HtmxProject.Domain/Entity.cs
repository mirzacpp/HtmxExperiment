namespace HtmxProject.Domain;

/// <summary>
/// Base entity
/// </summary>
/// <typeparam name="TKey">Base entity Id type</typeparam>
public abstract class Entity<TKey> where TKey : struct
{
    public TKey Id { get; set; }
}
