namespace Catalog.API.Exception
{
    public class ProductNotFoundException : FormatException
    {
        public ProductNotFoundException():base("Product Not Found!")
        {
            
        }
    }
}
