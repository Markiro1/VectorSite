namespace VectorSite.DL.Exceptions.UserExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string id)
            : base($"User not found by id: {id}")
        { }
    }
}
