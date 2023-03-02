namespace Ecommerce.API.Exceptions
{
    public abstract class CartException : Exception
    {
        protected CartException(string message) 
            : base(message)
        {   
        }
    }
}