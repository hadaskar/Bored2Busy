namespace IDAL
{
    
    using static DTO.Class1;
    public interface ICategoryREPO
        {
            List<CategoryDTO> GetAll();
            CategoryDTO GetCategoryByID(int id);
            void AddCategory(CategoryDTO category);
            void UpdateCategory(CategoryDTO category);
            void DeleteCategory(int id);

        }
        public interface ITaskREPO
        {
            List<TaskDTO> GetAll();
            TaskDTO GetTaskByID(int id);
            void AddTask(TaskDTO task);
            void UpdateTask(TaskDTO task);
            void DeleteTask(int id);
        }
        public interface IEmployeeREPO
        {
            List<EmployeeDTO> GetAll();
            EmployeeDTO GetEmployeeByID(int id);
            void AddEmployee(EmployeeDTO employee);
            void UpdateEmployee(EmployeeDTO employee);
            void DeleteEmployee(int id);
        }
    }

