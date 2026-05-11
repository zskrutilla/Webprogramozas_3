using MvcIoCDemo.Models;

namespace MvcIoCDemo.Data;

public interface IPositionRepository
{
    IEnumerable<Position> GetAll();
    void Add(string name);
}
