﻿using IdeoPrivateRoom.WebApi.Models.Enums;

namespace IdeoPrivateRoom.WebApi.Models.Responses;

public class VocationResponse
{
    public Guid Id { get; set; }
    public VocationUserResponse User { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ApprovalStatus Status { get; set; }
    public IEnumerable<VocationUserApprovalResponse> UserApprovalResponses { get; set; }
}
