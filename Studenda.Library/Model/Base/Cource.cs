namespace Studenda.Library.Model.Base;

/// <summary>
/// Курс.
/// </summary>
public class Course : Entity
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
    /// Идентификатор связанного факультета.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Связанный факультет.
    /// </summary>
    public Department Department { get; set; } = null!;

    /// <summary>
    /// Связанные группы.
    /// </summary>
    public List<Group> Groups { get; set; } = null!;
}
