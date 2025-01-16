namespace VectorSite.DL.Exceptions.UserExceptions
{
    public class UserCreationFailedException : Exception
    {
        public UserCreationFailedException()
            :base("User creation failed! Please check user details and try again.")
        { }
    }
}
