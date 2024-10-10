namespace WebApplication1.Models;

/// <summary>
/// Base entity of the app, all entites should inherit it.
/// </summary>
public abstract class BaseEntity
{
    public int? Id { get; set; }
}
