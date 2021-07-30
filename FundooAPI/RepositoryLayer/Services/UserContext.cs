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
            public DbSet<User> FundooNotes { get; set; }
        }
    }

