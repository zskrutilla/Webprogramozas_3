using Task01.Models;

namespace Task01.Data
{
    public class PositionRepository : IPositionRepository
    {
        private readonly List<Position> positions = new();

        public PositionRepository()
        {
            positions.Add(new Position { Name = "developer" });
            positions.Add(new Position { Name = "tester" });
            positions.Add(new Position { Name = "manager" });
        }

        public void Add(Position position)
        {
            if (position != null && !string.IsNullOrWhiteSpace(position.Name))
            {
                positions.Add(position);
            }
        }

        public List<Position> GetAll()
        {
            return positions;
        }
    }
}
