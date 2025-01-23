using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;


namespace BattleshipAPI.Infrastructure.Parsistence
{


    public class SqlDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<TwoPlayerGame> TwoPlayerGame { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
         {
         }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Professor
            modelBuilder.Entity<Users>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<TwoPlayerGame>()
                .HasKey(t => t.GameId);
        }
    }

    

}
