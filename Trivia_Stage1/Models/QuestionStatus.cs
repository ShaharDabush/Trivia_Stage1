using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("QuestionStatus")]
public partial class QuestionStatus
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(10)]
    public string Status { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
