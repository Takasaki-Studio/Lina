using FluentValidation;
using Lina.Database.Models;

namespace Lina.Test.Database.Models;

[TestClass]
public class ValidateBaseEntityTest
{
    [TestMethod]
    public async Task IsValid_ChecksIfIncorrectModelValidationIsWorking()
    {
        var model = new TestModel(string.Empty);
        Assert.IsFalse(await model.IsValid());
    }
    
    [TestMethod]
    public async Task IsValid_ChecksIfCorrectModelValidationIsWorking()
    {
        var model = new TestModel("Gabriel");
        Assert.IsTrue(await model.IsValid());
    }

    [TestMethod]
    public async Task GetErrors_ValidatesIfItReturnsValidationObjectSuccessfullyFalse()
    {
        var model = new TestModel(string.Empty);
        var validationResult = await model.GetErrors();
        
        Assert.IsFalse(validationResult.IsValid);
    }

    [TestMethod]
    public async Task Validate_ThrowsWhenBadValidation()
    {
        var model = new TestModel(string.Empty);
        await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
        {
            await model.Validate();
        });
    }

    [TestMethod]
    public async Task Validate_ChecksThatValidationDoesNotReturnAnErrorWhenEntityValidates()
    {
        var model = new TestModel("Lucas");
        await model.Validate();
    }
}

internal class TestModelValidator : AbstractValidator<TestModel>
{
    public TestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

internal class TestModel : ValidateBaseEntity<TestModel, TestModelValidator, int>
{
    public TestModel(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}