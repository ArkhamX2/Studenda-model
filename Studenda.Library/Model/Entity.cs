namespace Studenda.Library.Model;

/// <summary>
/// Модель стандартного объекта с соответствующей
/// таблицей в базе данных.
/// Реализация Table Per Class подхода.
/// </summary>
public abstract class Entity
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

    #endregion
}
