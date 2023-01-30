using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.ViewModels
{
    public class EmployeesVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ManagerId { get; set; }

        public int EmployeeId { get; set; }

    }


    public enum Status
    {
        Pending,
        OnProgress,
        Complete
    }
}
