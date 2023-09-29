using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }
        //
        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var BuscaProduto = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscaProduto is null)
                {
                    return null;
                }

                produto.Categoria = BuscaProduto;
            }

            await _context.Produtos.AddAsync(produto);
            
            await _context.SaveChangesAsync();

            return produto;
        }
        //
        public async Task Delete(Produto produto)
        {
            _context.Remove(produto);
            await _context.SaveChangesAsync();
        }
        //
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos
               .Include(t => t.Categoria)
               .ToListAsync();
        }
        //
        public async Task<Produto?> GetById(long id)
        {
            try
            {
                var ProdutoUpdate = await _context.Produtos
                    .Include(produto => produto.Categoria)
                    .FirstAsync(i => i.Id == id);

                return ProdutoUpdate;
            }
            catch
            {
                return null;
            }
        }
        //
        public async Task<IEnumerable<Produto>> GetByNomeOuConsole(string nome, string console)
        {
            return await _context.Produtos
                .Where(produto => produto.Nome == nome || produto.Console == console)
                .Include(p => p.Categoria)
                .ToListAsync();
        }
        //
        public async Task<IEnumerable<Produto>> GetByPrecoIntervalo(decimal min, decimal max)
        {
            return await _context.Produtos
                .Where(produto => produto.Preco >= min && produto.Preco <=max)
                .Include(p => p.Categoria)
                .ToListAsync();
        }
        //
        public async Task<Produto?> Update(Produto produto)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(produto.Id);

            if (ProdutoUpdate is null)
            {
                return null;
            }

            if (produto.Categoria is not null)
            {
                var BuscaTema = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (BuscaTema is null)
                {
                    return null;
                }
            }

            produto.Categoria = produto.Categoria is not null ? _context.Categorias.FirstOrDefault(t => t.Id == produto.Categoria.Id) : null;

            _context.Entry(ProdutoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }
    }
}
