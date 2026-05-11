using Task03.Models;

namespace Task03.Data
{
    public class ClassRepository : IClassRepository
    {
        private readonly List<SchoolClass> classes = new();

        public ClassRepository()
        {
            classes.Add(new SchoolClass { ClassName = "A1", RoomNumber = "101" });
            classes.Add(new SchoolClass { ClassName = "B2", RoomNumber = "202" });
        }

        public void Add(SchoolClass schoolClass)
        {
            if (schoolClass != null && !string.IsNullOrWhiteSpace(schoolClass.ClassName))
            {
                classes.Add(schoolClass);
            }
        }

        public List<SchoolClass> GetAll()
        {
            return classes;
        }
    }
}
