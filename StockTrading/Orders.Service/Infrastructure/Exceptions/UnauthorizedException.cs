using Orders.Service.Infrastructure.Enums;

namespace Orders.Service.Infrastructure.Exceptions
{
    public class UnauthorizedException : ForbiddenOperationException
    {
        public UnauthorizedException(
            string message,
            Exception? innerException = null) : base(
                SeverityEnum.Warning,
                message,
                innerException: innerException)
        {
        }
    }
}
