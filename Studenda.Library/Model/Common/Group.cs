using Studenda.Library.Model.Link;

namespace Studenda.Library.Model.Common;

/// <summary>
/// Группа.
/// </summary>
public class Group : Entity
{
    /*                   __ _                       _   _
     *   ___ ___  _ __  / _(_) __ _ _   _ _ __ __ _| |_(_) ___  _ __
     *  / __/ _ \| '_ \| |_| |/ _` | | | | '__/ _` | __| |/ _ \| '_ \
     * | (_| (_) | | | |  _| | (_| | |_| | | | (_| | |_| | (_) | | | |
     *  \___\___/|_| |_|_| |_|\__, |\__,_|_|  \__,_|\__|_|\___/|_| |_|
     *                        |___/
     *
     * Константы, задающие базовые конфигурации полей
     * и ограничения модели.
     */
    #region Configuration

    /// <summary>
    /// Минимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMin = 1;

    /// <summary>
    /// Максимальная длина поля <see cref="Name"/>.
    /// </summary>
    public const int NameLengthMax = 128;

    #endregion

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
    /// Идентификатор связанного объекта <see cref="Common.Course"/>.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;

    #endregion

    /// <summary>
    /// Связанный объект <see cref="Common.Course"/>.
    /// </summary>
    public Course Course { get; set; } = null!;

    /// <summary>
    /// Связанный объект <see cref="UserGroupLink"/>.
    /// </summary>
    public List<UserGroupLink> UserGroupLinks { get; set; } = null!;
}
