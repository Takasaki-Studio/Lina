using TakasakiStudio.Lina.Common;
using FluentValidation;

namespace Takasaki.Studio.Lina.Test.Commons;

[TestClass]
public class BaseValidatorTest
{
    [TestMethod]
    public async Task ValidateValidatorWorks()
    {
        var model = new ExampleModel
        {
            Test = string.Empty
        };
        
        Assert.IsFalse(await model.IsValid());
    }
}

public class ExampleModel : BaseValidator<ExampleModel>
{
    public required string Test { get; set; }

    protected override void SetupValidator(LinaAbstractValidator<ExampleModel> rules)
    {
        rules.RuleFor(x => x.Test).NotEmpty().NotNull();
    }
}