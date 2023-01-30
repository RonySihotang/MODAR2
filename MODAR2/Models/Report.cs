using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_reports")]
public class Report
{
    [Key, Column("id")]
    public string Id { get; set; }
    [Required, Column("title"), MaxLength(50)]
    public string Title { get; set; }
    [Required, Column("date")]
    public DateTime Date { get; set; }
    [Required, Column("progress_report"), MaxLength(50)]
    public string ProgressReport { get; set; }
    [Required, Column("description"), MaxLength(255)]
    public string Description { get; set; }
    [Required, Column("status")]
    public Status Status { get; set; }
    [Required, Column("done_by"), MaxLength(50)]
    public string DoneBy { get; set; }
    [Required, Column("project_id", TypeName = "nchar(5)")]
    public string ProjectId { get; set; }

    [JsonIgnore]
    // Relation
    [ForeignKey("ProjectId")]
    public ProjectList? ProjectList { get; set; }
}

