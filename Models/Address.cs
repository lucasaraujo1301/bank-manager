namespace BankManager.Models
{
    public enum TypeOfAddresses {
        Home,
        Work
    }
    public class Address {
        public Address (string street, string city, string state, string zipCode, TypeOfAddresses addressType) {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            AddressType = addressType;
        }

        public string Street { get; set;}
        public string City { get; set;}
        public string State { get; set;}
        public string ZipCode { get; set;}
        public TypeOfAddresses AddressType { get; set; }
    }
    
}