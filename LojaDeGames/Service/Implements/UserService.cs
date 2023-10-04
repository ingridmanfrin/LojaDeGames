using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
               .ToListAsync();
        }

        public async Task<User?> GetById(long id)
        {
            try
            {
                var Usuario = await _context.Users
                    .FirstAsync(i => i.Id == id);
                Usuario.Senha = "";
                return Usuario;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> GetByUsuario(string usuario)
        {
            var BuscaUsuario = await _context.Users
                         .Where(u => u.Usuario.Contains(usuario))
                         .FirstOrDefaultAsync();

            return BuscaUsuario;
        }

        public async Task<User?> Create(User usuario)
        {
            var BuscaUsuario = await GetByUsuario(usuario.Usuario);

            if (BuscaUsuario is not null)
            {
                return null;
            }
            if (usuario.Foto is null || usuario.Foto == "")
            {
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";
            }

            //definindo como que a minha senha vai ser criptografada
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, workFactor: 10);

            await _context.Users.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<User?> Update(User usuario)
        {
            var UserUpdate = await _context.Users.FindAsync(usuario.Id);

            if (UserUpdate is null)
            {
                return null;
            }

            if (usuario.Foto is null || usuario.Foto == "")
            {
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";
            }

            //definindo como que a minha senha vai ser criptografada
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, workFactor: 10);

            _context.Entry(UserUpdate).State = EntityState.Detached;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }

    }
}
