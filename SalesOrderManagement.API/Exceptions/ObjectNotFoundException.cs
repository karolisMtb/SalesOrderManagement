namespace SalesOrderManagement.API.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message)
        {
            
        }

        public ObjectNotFoundException()
        {
                
        }
    }
}
