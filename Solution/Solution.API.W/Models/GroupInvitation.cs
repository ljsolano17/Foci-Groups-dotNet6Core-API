﻿using System;
using System.Collections.Generic;

namespace Solution.API.W.Models;

public partial class GroupInvitation
{
    public int GroupInvitationId { get; set; }

    public string? FromUserId { get; set; }

    public int GroupId { get; set; }

    public string? ToUserId { get; set; }

    public DateTime SentDate { get; set; }

    public bool Accepted { get; set; }

    public virtual Group Group { get; set; } = null!;
}
