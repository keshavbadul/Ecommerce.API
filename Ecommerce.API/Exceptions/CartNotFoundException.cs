namespace Ecommerce.API.Exceptions
{
    public sealed class CartNotFoundException : CartException
    {
        public CartNotFoundException()
            : base("No cart was found for the given user")
        {
        }
    }
}