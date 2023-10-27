namespace LINQ_Intensive
{
    //test class
    internal class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public Department(int id, string departmentName)
        {
            Id = id;
            DepartmentName = departmentName;
        }
    }
}
