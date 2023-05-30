using Lina.Test.RepositoryFactory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lina.Test.RepositoryFactory.Database.Configuration;

public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.Property(x => x.Name).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
    }
}