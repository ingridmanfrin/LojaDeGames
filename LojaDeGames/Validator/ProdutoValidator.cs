using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(t => t.Nome)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(100);

            RuleFor(t => t.Descricao)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(1000);

            RuleFor(t => t.Console)
                   .NotEmpty()
                   .MinimumLength(2)
                   .MaximumLength(250);

            RuleFor(t => t.DataLancamento)
                   .NotEmpty();
            
            RuleFor(t => t.Preco)
                   .NotEmpty();
            
            RuleFor(t => t.Foto)
                   .NotEmpty()
                   .MinimumLength(2)
                   .MaximumLength(5000);
        }
    }
}
