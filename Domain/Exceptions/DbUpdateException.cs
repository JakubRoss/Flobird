namespace Domain.Exceptions
{
    public class DbUpdateException : Exception
    {
        public DbUpdateException(string message) : base(message)
        {
        }
    }
}
