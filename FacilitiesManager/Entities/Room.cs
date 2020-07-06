namespace Entities
{
    public class Room
    {
        public string Id { get; set; }

        public int Floor { get; set; }

        public string BuildingId { get; set; }

        public int Capacity { get; set; }

        public bool IsFull { get; set; }
    }

    public class DiscardThisClassAsItsForTrial : ITrial
    {
        public Toilet GetDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}
