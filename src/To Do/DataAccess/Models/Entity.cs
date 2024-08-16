using SQLite;

namespace To_Do.DataAccess.Models;

public class Entity
{
    [AutoIncrement, PrimaryKey]
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
