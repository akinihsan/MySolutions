using Farm.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Farm.Core.Entities.Base;

namespace Farm.Infrastructure.DBContext
{
    public class FarmDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Farm")
                          .LogTo(FarmDbContext.DebugLog);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
            builder.ApplyGlobalFilters<ISoftDelete>(e => e.IsDeleted == false);
        }

        public override int SaveChanges()
        {
            EntityBaseInterceptor();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityBaseInterceptor();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EntityBaseInterceptor();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EntityBaseInterceptor();

            return base.SaveChangesAsync(cancellationToken);
        }
        public static void DebugLog(string value) => Debug.WriteLine(value);

        public Microsoft.EntityFrameworkCore.DbSet<Animal> Animals { get; set; }
        // This can be used when the app save data into persistent databases rather than InMemoryDb to track some audit info and set default values
        private void EntityBaseInterceptor()
        {
            var entries = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Deleted || x.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is EntityBase)
                    {
                        entry.CurrentValues["ExternalId"] = Guid.NewGuid();
                        entry.CurrentValues["IsDeleted"] = false;
                        entry.CurrentValues["CreationTime"] = DateTime.Now;
                    }

                }
                else if (entry.State == EntityState.Deleted && entry.Entity is EntityBase)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                    entry.CurrentValues["LastModifiedTime"] = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified && entry.Entity is EntityBase)
                {
                    entry.CurrentValues["LastModifiedTime"] = DateTime.Now;
                }
            }
        }
    }
    public static class ModelBuilderExtension
    {
        public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
        {
            var entities = modelBuilder.Model
                .GetEntityTypes()
                .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
                .Select(e => e.ClrType);
            foreach (var entity in entities)
            {
                if (entity.BaseType != null)
                    if (entity.BaseType.Name == "EntityBase")
                    {
                        var newParam = Expression.Parameter(entity);
                        var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                        modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
                    }
            }
        }
    }
}
