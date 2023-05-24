using Studenda.Library.Model.Account;
using Studenda.Library.Model.Common;

namespace Studenda.Library.Model.Link;

/// <summary>
/// Связь многие ко многим для <see cref="Account.User"/> и <see cref="Common.Group"/>.
/// </summary>
public class UserGroupLink
{
    /*             _   _ _
     *   ___ _ __ | |_(_) |_ _   _
     *  / _ \ '_ \| __| | __| | | |
     * |  __/ | | | |_| | |_| |_| |
     *  \___|_| |_|\__|_|\__|\__, |
     *                       |___/
     *
     * Поля данных, соответствующие таковым в таблице
     * модели в базе данных.
     */
    #region Entity

    /// <summary>
    /// Идентификатор связанного объекта <see cref="Account.User"/>.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Идентификатор связанного объекта <see cref="Common.Group"/>.
    /// </summary>
    public int GroupId { get; set; }

    #endregion

    /// <summary>
    /// Связанный объект <see cref="Account.User"/>.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Связанный объект <see cref="Common.Group"/>.
    /// </summary>
    public Group Group { get; set; } = null!;
}
