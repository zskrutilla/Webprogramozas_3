using Task01.Models;

namespace Task01.Data
{
    public interface IPositionRepository
    {
        void Add(Position position);
        List<Position> GetAll();
    }
}
