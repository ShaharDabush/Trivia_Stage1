using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Index("Question1", Name = "UQ__Question__6B387A687FF151CD", IsUnique = true)]
public partial class Question
{
    [Key]
    [Column("QuestionID")]
    public int QuestionId { get; set; }

    [Column("Question")]
    [StringLength(100)]
    public string Question1 { get; set; } = null!;

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("SubjectID")]
    public int SubjectId { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    [ForeignKey("StatusId")]
    [InverseProperty("Questions")]
    public virtual QuestionStatus Status { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("Questions")]
    public virtual QuestionSubject Subject { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Questions")]
    public virtual User User { get; set; } = null!;
}
