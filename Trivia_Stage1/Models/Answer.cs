using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Answer
{
    [Key]
    [Column("AnswerID")]
    public int AnswerId { get; set; }

    [Column("Answer")]
    [StringLength(100)]
    public string Answer1 { get; set; } = null!;

    [Column("QuestionID")]
    public int QuestionId { get; set; }

    [Column("true_false")]
    public bool TrueFalse { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("Answers")]
    public virtual Question Question { get; set; } = null!;
}
