using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Model.Account;

namespace Studenda.Library.Data.Configuration.Model.Account;

/// <summary>
/// Конфигурация модели <see cref="User"/>.
/// </summary>
public class UserRoleConfiguration : EntityConfiguration<UserRole>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public UserRoleConfiguration(DatabaseConfiguration configuration) : base(configuration) { }

    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.Property(role => role.Name)
            .HasMaxLength(User.NameLengthMax)
            .IsRequired();

        builder.HasMany(role => role.Users)
            .WithOne(user => user.UserRole)
            .HasForeignKey(user => user.UserRoleId);

        base.Configure(builder);
    }
}
