using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entity.Models;
using Entity.Models.Blog;
using Entity.Models.Common;
using Entity.Models.File;
using Entity.Models.News;
using Entity.Models.Translation;
using Microsoft.EntityFrameworkCore;
using User = Entity.Models.Users.User;

namespace DatabaseBroker;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public  DbSet<FileModel> Files { get; set; }
    public  DbSet<Views> Views { get; set; }
    public  DbSet<Translation> Translations { get; set; }
    public  DbSet<User> Users { get; set; }
    public  DbSet<OurService> OurServices { get; set; }
    public  DbSet<OurResources> OurResources { get; set; }
    public  DbSet<OurValuedClients> OurValuedClients { get; set; }
    public  DbSet<OurCategories> OurCategories { get; set; }
    public  DbSet<OurTeam> OurTeams { get; set; }
    public  DbSet<Statistics> Statistics { get; set; }
    public  DbSet<OurPartners> OurPartners { get; set; }
    public  DbSet<NewsCategory> NewsCategories { get; set; }
    public  DbSet<ResourceCategory> ResourceCategories { get; set; }
    public  DbSet<Tags> Tags { get; set; }
    public  DbSet<News> News { get; set; }
    public  DbSet<BlogModel> BlogModels { get; set; }
    public  DbSet<Publisher> Publishers { get; set; }
    public  DbSet<Resources> Resources { get; set; }
    public  DbSet<Client> Clients { get; set; }
    public  DbSet<ImageModel> ImageModels { get; set; }
    public  DbSet<ImageCategory> ImageCategories { get; set; }
    public  DbSet<PicturesModel> PicturesModels { get; set; }


    // private void TrackActionsAt()
    // {
    //     foreach (var entity in this.ChangeTracker.Entries()
    //                  .Where(x => x.State == EntityState.Added && x.Entity is AuditableModelBase<int>))
    //     {
    //         var model = (AuditableModelBase<int>)entity.Entity;
    //         model.CreatedAt = DateTime.Now;
    //         model.UpdatedAt = model.CreatedAt;
    //     }
    //
    //     foreach (var entity in this.ChangeTracker.Entries()
    //                  .Where(x => x.State == EntityState.Modified && x.Entity is AuditableModelBase<int>))
    //     {
    //         var model = (AuditableModelBase<int>)entity.Entity;
    //         model.UpdatedAt = DateTime.Now;
    //     }
    // }

    public override int SaveChanges()
    {
        //TrackActionsAt();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
       // TrackActionsAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        //TrackActionsAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //TrackActionsAt();
        return base.SaveChangesAsync(cancellationToken);
    }
   
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Skill>()
        //     .HasOne(s => s.TeamMember) 
        //     .WithMany(t => t.Skills) 
        //     .HasForeignKey(s => s.TeamMemberId);
        
        #region Configurations related to MultiLanguageField

        //Configuring all MultiLanguage fields over entities
        var mlfs = this.GetType().GetProperties()
            .Where(x => x.PropertyType.IsGenericType)
            .Where(x => x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Select(x => x.PropertyType.GetGenericArguments().FirstOrDefault())
            .SelectMany(x => x.GetProperties())
            .Where(x => x.PropertyType == typeof(MultiLanguageField));

        foreach (var multiLanguageField in mlfs)
            modelBuilder
                .Entity(multiLanguageField.ReflectedType!)
                .Property(multiLanguageField.PropertyType, multiLanguageField.Name)
                .HasColumnType("jsonb");

        #endregion
        
        
        
       
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //
    //    
    //     var mlfs = this.GetType().GetProperties()
    //         .Where(x => x.PropertyType.IsGenericType)
    //         .Where(x => x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
    //         .Select(x => x.PropertyType.GetGenericArguments().FirstOrDefault())
    //         .SelectMany(x => x.GetProperties())
    //         .Where(x => x.PropertyType == typeof(MultiLanguageField));
    //
    //     foreach (var multiLanguageField in mlfs)
    //         modelBuilder
    //             .Entity(multiLanguageField.ReflectedType!)
    //             .Property(multiLanguageField.PropertyType, multiLanguageField.Name)
    //             .HasColumnType("jsonb");
    //     
    //     var helpers = modelBuilder
    //         .Model
    //         .GetEntityTypes()
    //         .Where(x => x.ClrType.BaseType is not null && x.ClrType.BaseType!.IsGenericType &&
    //                     x.ClrType.BaseType?.GetGenericTypeDefinition() == typeof(HelperModelBase<>));
    //
    //     foreach (var helperType in helpers)
    //         modelBuilder
    //             .Entity(helperType.ClrType)
    //             .HasIndex(nameof(HelperModelBase<long>.Code))
    //             .IsUnique();
    //
    // }
    //
}