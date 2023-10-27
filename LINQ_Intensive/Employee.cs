namespace LINQ_Intensive
{
    //test class
    internal class Employee
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public Employee(int id, string name)
        {
            DepartmentId = id;
            Name = name;
        }
    }
}
