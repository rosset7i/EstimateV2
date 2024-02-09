using Bogus;

namespace Estimate.UnitTest.TestUtils;

public static class Constants
{
    private static readonly Faker Faker = new();

    public static class User
    {
        public static readonly string Name = Faker.Person.FullName;
        public static readonly string Email = Faker.Person.Email;
        public static readonly string Phone = Faker.Phone.PhoneNumber("99999999999");
        public static readonly string Token = Faker.Random.Hash();
    }

    public static class Supplier
    {
        public static readonly Guid Guid = Faker.Random.Guid();
        public static readonly string Name = Faker.Name.FirstName();
    }

    public static class Product
    {
        public static readonly Guid Guid = Faker.Random.Guid();
        public static readonly string Name = Faker.Name.FirstName();
    }

    public static class Estimate
    {
        public static readonly Guid Guid = Faker.Random.Guid();
        public static readonly string Name = Faker.Name.FirstName();
    }
}