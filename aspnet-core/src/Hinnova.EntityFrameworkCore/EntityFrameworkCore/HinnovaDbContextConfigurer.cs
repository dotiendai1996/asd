using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Hinnova.EntityFrameworkCore
{
    public static class HinnovaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HinnovaDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HinnovaDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}