namespace Task02.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Category { get; set; }

        public int Credit { get; set; }

        public bool IsOnline { get; set; }
    }
}
