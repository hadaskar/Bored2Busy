using System;
using System.Collections.Generic;

namespace DAL.Models;

using DTO;
using IDAL;
public partial class Task : ITaskREPO
{
    public int TaskId { get; set; }

    public int? CategoryId { get; set; }

    public string TaskName { get; set; } = null!;

    public int? TaskDuration { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
