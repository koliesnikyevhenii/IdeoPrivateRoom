﻿namespace IdeoPrivateRoom.WebApi.Models.Requests;

public class CreateVocationRequest
{
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
