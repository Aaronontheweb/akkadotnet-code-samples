using System;

namespace ETLActors.Shared.State
{
    /// <summary>
    /// Represents an <see cref="Address"/>.
    /// </summary>
    public class Address
    {
        public String Name { get; private set; }
        public String Line1 { get; private set; }
        public String City { get; private set; }
        public String Region { get; private set; }
        public String PostalCode { get; private set; }
        public String CountryCode { get; private set; }

        public Address(string name, string line1, string city, string region, string postalCode, string countryCode)
        {
            Name = name;
            Line1 = line1;
            City = city;
            Region = region;
            PostalCode = postalCode;
            CountryCode = countryCode;
        }
    }
}