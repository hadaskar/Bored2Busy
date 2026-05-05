namespace DTO

{
    using IDAL;
    public class Class1
    {
      
        public class CategoryDTO : 
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }

        }
        public class TaskDTO
        {

            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public  CategoryDTO Category { get; set; }
            public int? TaskDuration { get; set; }
        }
        public class EmployeeDTO
        {
            public int EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public int TaskID { get; set; }

            public TaskDTO Task { get; set; }


        }
    }
}
