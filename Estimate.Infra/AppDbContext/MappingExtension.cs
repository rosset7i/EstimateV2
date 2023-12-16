using Estimate.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estimate.Infra.AppDbContext;

public static class MappingExtension
{
    public static void IsValueObject<TClass>(this ComplexPropertyBuilder<TClass> builder)
        where TClass : ValueObject<TClass>
    {
        builder.Ignore(e => e.ClassLevelCascadeMode);
        builder.Ignore(e => e.RuleLevelCascadeMode);
        builder.Ignore(e => e.CascadeMode);
    }
}