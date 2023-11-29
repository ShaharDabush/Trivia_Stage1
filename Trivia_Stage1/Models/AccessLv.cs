using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("AccessLV")]
public partial class AccessLv
{
    [Key]
    public int AccessId { get; set; }

    [Column("Access_level")]
    [StringLength(10)]
    public string AccessLevel { get; set; } = null!;

    [InverseProperty("Access")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
