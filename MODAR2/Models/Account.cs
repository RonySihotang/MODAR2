using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_accounts")]
public class Account
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("password")]
    public string Password { get; set; }
    [Column("otp")]
    public int OTP { get; set; }
    [Column("expired_token")]
    public DateTime ExpiredToken { get; set; }
    [Column("is_used")]
    public bool IsUsed { get; set; }


    // Relation
    [JsonIgnore]
    public Employee? Employee { get; set; }


    public ICollection<AccountRole>? AccountRoles { get; set; }
}


