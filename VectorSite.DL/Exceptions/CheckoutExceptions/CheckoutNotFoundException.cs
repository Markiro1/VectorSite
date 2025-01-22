using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorSite.DL.Exceptions.CheckoutExceptions
{
    public class CheckoutNotFoundException : Exception
    {
        public CheckoutNotFoundException(int checkoutId) 
            : base($"Checkout not found by id: {checkoutId}")
        { }
    }
}
