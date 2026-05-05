using System;
using System.Collections.Generic;

namespace DAL.Models;
using DTO;
using IDAL;

public partial class Employee: IEmployeeREPO
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }

    public void AddEmployee(Class1.EmployeeDTO employee)
    {
        Employee empEntity = new Employee
        {
            EmployeeId = employee.EmployeeID,
            EmployeeName = employee.EmployeeName,
            TaskId = employee.TaskID
        };

        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                db.Employees.Add(empEntity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהוספת עובד למסד הנתונים", ex);
            }
        }
    }

    public void DeleteEmployee(int id)
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var emp = db.Employees.Find(id);
                if (emp == null)
                    throw new KeyNotFoundException($"Employee with id {id} not found.");

                db.Employees.Remove(emp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה במחיקת עובד מהמסד", ex);
            }
        }
    }

    public List<Class1.EmployeeDTO> GetAll()
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var emps = db.Employees.ToList();

                var result = emps.Select(e => new Class1.EmployeeDTO
                {
                    EmployeeID = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    TaskID = (int)e.TaskId,
                    // reuse Task DAL method to obtain the TaskDTO (avoids duplicate mapping code)
                    Task = e.TaskId.HasValue ? new Task().GetTaskByID(e.TaskId.Value) : null
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת כל העובדים מהמסד", ex);
            }
        }
    }

    public Class1.EmployeeDTO GetEmployeeByID(int id)
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var e = db.Employees.FirstOrDefault(x => x.EmployeeId == id);
                if (e == null)
                    throw new KeyNotFoundException($"Employee with id {id} not found.");

                return new Class1.EmployeeDTO
                {
                    EmployeeID = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    TaskID = (int)e.TaskId,
                    Task = e.TaskId.HasValue ? new Task().GetTaskByID(e.TaskId.Value) : null
                };
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת עובד מהמסד", ex);
            }
        }
    }
    public void UpdateEmployee(Class1.EmployeeDTO employee)
    {
        using (BornDbContext db = new BornDbContext())
        {
            try
            {
                var existing = db.Employees.Find(employee.EmployeeID);
                if (existing == null)
                    throw new KeyNotFoundException($"Employee with id {employee.EmployeeID} not found.");

                existing.EmployeeName = employee.EmployeeName;
                existing.TaskId = employee.TaskID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בעדכון עובד במסד הנתונים", ex);
            }
        }
    }
}
