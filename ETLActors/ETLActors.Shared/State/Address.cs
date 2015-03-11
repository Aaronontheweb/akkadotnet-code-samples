﻿using System;

namespace ETLActors.Shared.State
{
    /// <summary>
    /// Represents an <see cref="Address"/>.
    /// </summary>
    public class Address
    {
        // TODO remove
        public Address() : this("andrew", "1261 granville", "", "la", "CA", "90025", "US", "3108965198")
        {
        }

        public String Name { get; private set; }
        public String Line1 { get; private set; }
        public String Line2 { get; private set; }
        public String City { get; private set; }
        public String Region { get; private set; }
        public String PostalCode { get; private set; }
        public String CountryCode { get; private set; }
        public String PhoneNumber { get; private set; }

        public Address(string name, string line1, string line2, string city, string region, string postalCode, string countryCode, string phoneNumber)
        {
            Name = name;
            Line1 = line1;
            Line2 = line2;
            City = city;
            Region = region;
            PostalCode = postalCode;
            CountryCode = countryCode;
            PhoneNumber = phoneNumber;
        }
    }
}