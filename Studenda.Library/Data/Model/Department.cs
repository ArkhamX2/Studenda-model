namespace Studenda.Library.Model;

/// <summary>
/// Факультет.
/// </summary>
public class Department : Entity
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
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Связанные курсы.
    /// </summary>
    public ICollection<Course> Courses { get; set; } = null!;
}
