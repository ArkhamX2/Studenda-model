using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Model.Account;

namespace Studenda.Library.Data.Configuration.Model.Account;

/// <summary>
/// Конфигурация модели <see cref="User"/>.
/// </summary>
public class UserConfiguration : EntityConfiguration<User>
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public UserConfiguration(DatabaseConfiguration configuration) : base(configuration) { }

    /// <summary>
    /// Задать конфигурацию для модели.
    /// </summary>
    /// <param name="builder">Набор интерфейсов настройки модели.</param>
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Name)
            .HasMaxLength(User.NameLengthMax)
            .IsRequired();

        builder.Property(user => user.Surname)
            .HasMaxLength(User.SurnameLengthMax);

        builder.Property(user => user.Patronymic)
            .HasMaxLength(User.PatronymicLengthMax);

        builder.Property(user => user.Email)
            .HasMaxLength(User.EmailLengthMax)
            .IsRequired();

        builder.Property(user => user.PasswordHash)
            .HasMaxLength(User.PasswordHashLengthMax)
            .IsRequired();

        builder.HasOne(user => user.UserRole)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.UserRoleId)
            .IsRequired();

        builder.HasMany(user => user.UserGroupLinks)
            .WithOne(link => link.User)
            .HasForeignKey(link => link.UserId);

        base.Configure(builder);
    }
}
