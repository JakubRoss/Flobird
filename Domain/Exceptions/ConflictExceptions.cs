namespace Domain.Exceptions
{
    public class ConflictExceptions : Exception
    {
        public ConflictExceptions(string massage) : base(massage) { }
    }
}
