using FluentValidation;

namespace TakasakiStudio.Lina.Common;

public class LinaAbstractValidator<TModel> : AbstractValidator<TModel>
{
    public delegate void ValidationBuilder(LinaAbstractValidator<TModel> builder); 
    internal LinaAbstractValidator(ValidationBuilder constructorBuilder)
    {
        constructorBuilder(this);
    }
}