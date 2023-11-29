using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Index("UserMail", Name = "UQ__Users__52ABC69B2D494317", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(90)]
    public string UserMail { get; set; } = null!;

    [StringLength(20)]
    public string UserName { get; set; } = null!;

    [StringLength(30)]
    public string Password { get; set; } = null!;

    public int AccessId { get; set; }

    public int? Score { get; set; }

    public int? TotalScore { get; set; }

    public int? QuastionsAdded { get; set; }

    [ForeignKey("AccessId")]
    [InverseProperty("Users")]
    public virtual AccessLv Access { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
