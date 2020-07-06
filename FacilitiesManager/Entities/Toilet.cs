namespace Entities
{
    public class Toilet : Room
    {
        public ToiletType ToiletType { get; }

        public Toilet(ToiletType toiletType)
        {
            this.ToiletType = toiletType;
        }

        public string DiscardThisProperty { get; set; }
    }

    interface ITrial
    {
        Toilet GetDetails();
    }
}
