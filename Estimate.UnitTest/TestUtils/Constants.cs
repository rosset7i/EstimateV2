using AutoFixture;
using Bogus;
using Estimate.Core.Estimates.Dtos;

namespace Estimate.UnitTest.TestUtils;

public static class Constants
{
    private static readonly Faker Faker = new();
    private static readonly Fixture Fixture = new();

    public static class User
    {
        public static readonly string Name = Faker.Person.FullName;
        public static readonly string Email = Faker.Person.Email;
        public static readonly string Phone = Faker.Phone.PhoneNumber("99999999999");
        public static readonly string Password = Faker.Internet.Password();
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
        public static readonly List<UpdateEstimateProductsRequest> UpdateEstimateProductsRequests = Fixture
            .CreateMany<UpdateEstimateProductsRequest>()
            .ToList();
    }
}