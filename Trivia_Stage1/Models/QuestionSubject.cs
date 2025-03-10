﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class QuestionSubject
{
    [Key]
    public int SubjectId { get; set; }

    [StringLength(50)]
    public string Subject { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
