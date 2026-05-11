using Task03.Models;

namespace Task03.Data
{
    public interface IClassRepository
    {
        void Add(SchoolClass schoolClass);
        List<SchoolClass> GetAll();
    }
}
