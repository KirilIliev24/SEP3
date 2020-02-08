using Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Data.DataAccess;

namespace Data.Data
{
    class ThemisContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkGroupDeveloper> WorkGroupDevelopers { get; set; }
        public DbSet<InvitationModel> Invitation { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<BackLogItem> BackLogItems { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Themis;Integrated Security=True");
            base.OnConfiguring(builder);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
