using BattleshipAPI.Application.DTOs;
using BattleshipAPI.Domain.Entites;
using BattleshipAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleshipAPI.Infrastructure.Parsistence
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
         {
         }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Professor
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            //modelBuilder.Entity<TwoPlayerGame>()
            //    .HasKey(t => t.GameId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<Ships> Ships { get; set; }


        //public DbSet<TwoPlayerGame> TwoPlayerGames { get; set; }
        //public DbSet<SoloGame> SoloGame { get; set; }
    }
}
