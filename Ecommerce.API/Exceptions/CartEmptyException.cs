namespace Ecommerce.API.Exceptions
{
    public sealed class CartEmptyException : CartException
    {
        public CartEmptyException()
            : base("The cart is empty")
        {
        }
    }
}