using MvcIoCDemo.Models;

namespace MvcIoCDemo.Data;

public class PositionRepository : IPositionRepository
{
    private readonly List<Position> positions;
    private int nextId;

    public PositionRepository()
    {
        positions = new List<Position>
        {
            new Position { Id = 1, Name = "Developer" },
            new Position { Id = 2, Name = "Tester" },
            new Position { Id = 3, Name = "Project Manager" }
        };

        nextId = positions.Max(p => p.Id) + 1;
    }

    public IEnumerable<Position> GetAll()
    {
        return positions;
    }

    public void Add(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        positions.Add(new Position
        {
            Id = nextId++,
            Name = name.Trim()
        });
    }
}
