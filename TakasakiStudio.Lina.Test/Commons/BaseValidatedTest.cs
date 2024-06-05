using TakasakiStudio.Lina.Common;
using FluentValidation;

namespace TakasakiStudio.Lina.Test.Commons;

[TestClass]
public class BaseValidatedTest
{
    [TestMethod]
    public async Task ValidateIsValidWorks()
    {
        var model = new ExampleModel
        {
            Test = string.Empty
        };

        Assert.IsFalse(await model.IsValid());
    }

    [TestMethod]
    public async Task ValidateValidateWorks()
    {
        var model = new ExampleModel
        {
            Test = string.Empty
        };

        await Assert.ThrowsExceptionAsync<ValidationException>(async () => await model.Validate());
    }
    
    [TestMethod]
    public async Task ValidateGetErrorsWorks()
    {
        var model = new ExampleModel
        {
            Test = string.Empty
        };

        Assert.IsNotNull(await model.GetErrors());
    }
    
    [TestMethod]
    public async Task ValidateOptionalIsValidWorks()
    {
        var model = new ExampleModel
        {
            Test = "test"
        };

        Assert.IsFalse(await model.IsValid("optional"));
    }

    [TestMethod]
    public async Task ValidateOptionalValidateWorks()
    {
        var model = new ExampleModel
        {
            Test = "test"
        };

        await Assert.ThrowsExceptionAsync<ValidationException>(async () => await model.Validate("optional"));
    }
    
    [TestMethod]
    public async Task ValidateOptionalGetErrorsWorks()
    {
        var model = new ExampleModel
        {
            Test = "test"
        };

        Assert.IsNotNull(await model.GetErrors("optional"));
    }
}

public class ExampleModel : BaseValidated<ExampleModel>
{
    public required string Test { get; set; }
    public string? Optional { get; set; }

    protected override void SetupValidator(LinaAbstractValidator<ExampleModel> rules)
    {
        rules.RuleSet("optional", () =>
        {
            rules.RuleFor(x => x.Optional).NotEmpty().NotNull();
        });
        
        rules.RuleFor(x => x.Test).NotEmpty().NotNull();
    }
}