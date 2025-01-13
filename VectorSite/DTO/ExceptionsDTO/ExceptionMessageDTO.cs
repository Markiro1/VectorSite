namespace VectorSite.DTO.ExceptionsDTO
{
    public class ExceptionMessageDTO
    {
        public string Message { get; set; }

        public ExceptionMessageDTO(string message) 
        { 
            this.Message = message;
        }
    }
}
