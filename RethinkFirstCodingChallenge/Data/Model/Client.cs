namespace RethinkFirstCodingChallenge.Data.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthdate { get; set; }
        public GenderTypes Gender { get; set; }
        public DateTime? Removed { get; set; }
    }

    public enum GenderTypes
    {
        M,
        F,
        O
    }
}
