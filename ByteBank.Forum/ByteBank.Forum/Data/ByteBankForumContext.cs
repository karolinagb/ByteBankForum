using ByteBank.Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteBank.Forum.Data
{
    public class ByteBankForumContext : IdentityDbContext<UsuarioAplicacao>
    {
        public ByteBankForumContext(DbContextOptions<ByteBankForumContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
