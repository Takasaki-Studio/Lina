using Lina.UtilsExtensions;

namespace Lina.Database.Models;

public abstract class BaseEntity<TPkType>
{
    public TPkType Id { get; set; } = default!;
}