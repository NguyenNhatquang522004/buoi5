namespace buoi5.Repository
{
    public class StudentSearchViewModel
    {
        public string? SearchString { get; set; }
        public string? GenderFilter { get; set; }
        public string? NganhFilter { get; set; }
        public string? SortOrder { get; set; }
        public IEnumerable<Models.SinhVien> Students { get; set; } = new List<Models.SinhVien>();
        public int TotalCount { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }
}
