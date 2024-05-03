﻿using BooksManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        { 

        }

        DbSet<Category> Categories { get; set; }
    }
}