using FluentValidation;
using LojaDeGames.Model;
using LojaDeGames.Service;
using LojaDeGames.Service.Implements;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeGames.Controllers
{
    [Route("~/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoService produtoService, IValidator<Produto> produtoValidator)
        {
            _produtoService = produtoService;
            _produtoValidator = produtoValidator;
        }
        //
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }
        //
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _produtoService.GetById(id);

            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);
        }
        //
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);
            }

            var Resposta = await _produtoService.Create(produto);

            if (Resposta is null)
            {
                return BadRequest("Produto e/ou Categoria não encontrados!");
            }

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }
        //
        [HttpGet("nome/{nome}/ouconsole/{console}")]
        public async Task<ActionResult> GetByNomeOuConsole(string nome, string console)
        {
            return Ok(await _produtoService.GetByNomeOuConsole(nome, console));
        }
        //
        [HttpGet("preco_inicial/{min}/preco_final/{console}")]
        public async Task<ActionResult> GetByPrecoIntervalo(decimal min, decimal max)
        {
            return Ok(await _produtoService.GetByPrecoIntervalo(min, max));
        }
        //
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if (produto.Id == 0)
            {
                return BadRequest("Id do Produto é inválido!");
            }

            var validarPostagem = await _produtoValidator.ValidateAsync(produto);

            if (!validarPostagem.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);
            }

            var Resposta = await _produtoService.Update(produto);

            if (Resposta is null)
            {
                return NotFound("Produto e/ou Categoria não encontrados!");
            }
            return Ok(Resposta);
        }
        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            //para saber se o id existe 
            var BuscaProduto = await _produtoService.GetById(id);

            if (BuscaProduto is null)
            {
                return NotFound("Produto não foi encontrado!");
            }
            await _produtoService.Delete(BuscaProduto);
            return NoContent();
        }




    }
}
