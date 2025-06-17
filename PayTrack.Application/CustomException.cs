﻿namespace PayTrack.Application;

public class CustomException : Exception
{
    public int StatusCode { get; }

    public CustomException(string message, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
