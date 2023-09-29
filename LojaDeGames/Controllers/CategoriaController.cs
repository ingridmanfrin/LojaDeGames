using FluentValidation;
using LojaDeGames.Model;
using LojaDeGames.Service;
using LojaDeGames.Service.Implements;
using LojaDeGames.Validator;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeGames.Controllers
{
    [Route("~/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IValidator<Categoria> _categoriaValidator;

        public CategoriaController(ICategoriaService categoriaService, IValidator<Categoria> categoriaValidator)
        {
            _categoriaService = categoriaService;
            _categoriaValidator = categoriaValidator;
        }
        //
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriaService.GetAll());
        }
        //
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _categoriaService.GetById(id);

            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);
        }
        //
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult> GetByTipo(string tipo)
        {
            return Ok(await _categoriaService.GetByTipo(tipo));
        }
        //
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Categoria categoria)
        {
            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);
            }

            await _categoriaService.Create(categoria);

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }
        //
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                return BadRequest("Id da Categoria é inválido!");
            }

            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);
            }

            var Resposta = await _categoriaService.Update(categoria);

            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);
        }
        //
        [HttpDelete("{id}")]
        //usamos interface pq delite retorna void
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaCategoria = await _categoriaService.GetById(id);

            if (BuscaCategoria is null)
            {
                return NotFound("Categoria não foi encontrado!");
            }
            await _categoriaService.Delete(BuscaCategoria);
            return NoContent();
        }
    }
}
