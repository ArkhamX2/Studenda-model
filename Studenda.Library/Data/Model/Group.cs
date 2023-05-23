namespace Studenda.Library.Model;

/// <summary>
/// Группа.
/// </summary>
public class Group : Entity
{
    /// <summary>
    /// Минимальная длина названия.
    /// </summary>
    public const int NameLengthMin = 1;

    /// <summary>
    /// Максимальная длина названия.
    /// </summary>
    public const int NameLengthMax = 128;

    /// <summary>
    /// Идентификатор связанного курса.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Связанный курс.
    /// </summary>
    public Course Course { get; set; } = null!;
}
