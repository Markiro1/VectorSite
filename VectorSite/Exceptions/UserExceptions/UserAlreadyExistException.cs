namespace VectorSite.Exceptions.UserExceptions
{
    public class UserAlreadyExistException : Exception
    {

        public UserAlreadyExistException(string phoneNumber) 
            : base($"User already exists with phone number : {phoneNumber}")
        { 
        }
    }
}
