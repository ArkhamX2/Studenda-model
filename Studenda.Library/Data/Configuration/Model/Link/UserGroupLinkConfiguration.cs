using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Model.Link;

namespace Studenda.Library.Data.Configuration.Model.Link;

/// <summary>
/// Конфигурация модели <see cref="UserGroupLink"/>.
/// </summary>
public class UserGroupLinkConfiguration : IEntityTypeConfiguration<UserGroupLink>
{
    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public void Configure(EntityTypeBuilder<UserGroupLink> builder)
    {
        builder.HasKey(link => new
        {
            link.UserId,
            link.GroupId
        });

        builder.HasOne(link => link.User)
            .WithMany(user => user.UserGroupLinks)
            .HasForeignKey(link => link.UserId);

        builder.HasOne(link => link.Group)
            .WithMany(group => group.UserGroupLinks)
            .HasForeignKey(link => link.GroupId);
    }
}
