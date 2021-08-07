using CommonLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    
    
        public class UserContext : DbContext
        {
            public UserContext(DbContextOptions options)
                : base(options)
            {

            }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
        public DbSet<User> FundooNotes { get; set; }
        public DbSet<Notes> Notes { get; set; }
    }
    }

