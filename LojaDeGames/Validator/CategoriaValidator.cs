using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(t => t.Tipo)
                    .NotEmpty()
                    .MinimumLength(10)
                    .MaximumLength(250);

        }
    }
}
