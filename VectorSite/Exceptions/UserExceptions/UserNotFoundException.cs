namespace VectorSite.Exceptions.UserExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
            : base($"User not found by email: {email}")
        { }
    }
}
