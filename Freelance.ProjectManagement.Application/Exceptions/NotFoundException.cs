namespace Freelance.ProjectManagement.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public override string Message { get; }
        public NotFoundException(string message = "Requested resource is not found.")
        {
            Message = message;
        }
    }
}
