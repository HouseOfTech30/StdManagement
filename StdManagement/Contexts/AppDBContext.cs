using Microsoft.EntityFrameworkCore;
using StdManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StdManagement.Contexts
{
    public class AppDBContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CDGSPUM\\SQLEXPRESS;Initial Catalog=TestDemo;Integrated Security=True;Trust Server Certificate=True");
            }
        }
    }
}
