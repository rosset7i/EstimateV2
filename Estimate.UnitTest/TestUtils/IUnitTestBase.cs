namespace Estimate.UnitTest.TestUtils;

public interface IUnitTestBase<TClass, TMocks>
{
    TMocks GetMocks();

    TClass GetClass(TMocks mocks);
}