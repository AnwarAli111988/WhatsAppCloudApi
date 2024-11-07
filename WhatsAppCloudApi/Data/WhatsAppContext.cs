using Microsoft.EntityFrameworkCore;

namespace WhatsAppCloudApi.Data
{
    public class WhatsAppContext : DbContext
    {
        public WhatsAppContext(DbContextOptions<WhatsAppContext> options) : base(options) { }

        public DbSet<UserResponse> UserResponses { get; set; }
    }
}
