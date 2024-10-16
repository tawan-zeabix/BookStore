namespace API.Helpers;

public class HttpException : Exception
{
    public int StatusCode { get; set; }

    public HttpException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
    
}
public class BadRequestException : HttpException
{
    public BadRequestException(string message = "Bad Request") 
        : base(400, message) { }
}

public class UnauthorizedException : HttpException
{
    public UnauthorizedException(string message = "Unauthorized") 
        : base(401, message) { }
}
    
public class ForbiddenException : HttpException
{
    public ForbiddenException(string message = "Forbidden") 
        : base(403, message) { }
}
    
public class NotFoundException : HttpException
{
    public NotFoundException(string message = "Resource Not Found") 
        : base(404, message) { }
}
    
public class ConflictException : HttpException
{
    public ConflictException(string message = "Conflict") 
        : base(409, message) { }
}
    
public class InternalServerErrorException : HttpException
{
    public InternalServerErrorException(string message = "Internal Server Error") 
        : base(500, message) { }
}