namespace DotLive01.Entities
{
    public record Address(string Street, string City, string State, string ZipCode)
    {
        public string GetFullAddress()
            => $"Address: {Street ?? "N/A"}, {City ?? "N/A"}, {State ?? "N/A"}, {ZipCode ?? "N/A"}";
    }

    //public class Address
    //{
    //    public Address(string street, string city, string state, string zipCode)
    //    {
    //        Street = street;
    //        City = city;
    //        State = state;
    //        ZipCode = zipCode;
    //    }

    //    public string Street { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string ZipCode { get; set; }

    //    public string GetFullAddress()
    //        => $"Address: {Street ?? "N/A"}, {City ?? "N/A"}, {State ?? "N/A"}, {ZipCode ?? "N/A"}";

    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null || GetType() != obj.GetType())
    //        {
    //            return false;
    //        }

    //        if (obj is not Address other)
    //        {
    //            return false;
    //        }

    //        return Street == other.Street
    //            && City == other.City
    //            && State == other.State
    //            && ZipCode == other.ZipCode;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return HashCode.Combine(Street, City, State, ZipCode);
    //    }
    //}
}
