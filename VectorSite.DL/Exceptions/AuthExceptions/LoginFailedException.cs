namespace VectorSite.DL.Exceptions.AuthExceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
            : base("Failed login. Incorrect data.")
        { }
    }
}
