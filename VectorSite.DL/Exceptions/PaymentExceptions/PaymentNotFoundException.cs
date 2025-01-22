namespace VectorSite.DL.Exceptions.PaymentExceptions
{
    public class PaymentNotFoundException : Exception
    {

        public PaymentNotFoundException(int paymentId)
                : base($"Payment not found by id: {paymentId}")
        { }
    }
}
