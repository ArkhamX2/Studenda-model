namespace Studenda.Library.Model;

/// <summary>
/// Модель стандартного объекта с соответствующей
/// таблицей в базе данных.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Дата создания.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Дата обновления.
	/// </summary>
	public DateTime? UpdatedAt { get; set; }
}
