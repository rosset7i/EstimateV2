using Estimate.Domain.Entities.Base;
using FluentValidation;

namespace Estimate.Domain.Entities.ValueObjects;

public class Price : ValueObject<Price>
{
    public decimal UnitPrice { get; set; }
    public double Quantity { get; set; }
    public decimal TotalPrice => CalculateTotalPrice();

    public Price(
        decimal unitPrice,
        double quantity)
    {
        UnitPrice = unitPrice;
        Quantity = quantity;
        ValidateObject();
    }

    private decimal CalculateTotalPrice() =>
        UnitPrice * (decimal)Quantity;

    protected sealed override void ValidateObject()
    {
        RuleFor(e => e.UnitPrice)
            .NotEmpty()
            .GreaterThan(0.01m);

        RuleFor(e => e.Quantity)
            .NotEmpty()
            .GreaterThan(0.01);

        ThrowIfAny(Validate(this));
    }
}