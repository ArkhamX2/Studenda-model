namespace Studenda.Core.Data.Configuration;

/// <summary>
/// Конфигурация баз данных MySQL и MariaDB.
/// </summary>
public class MysqlConfiguration : DatabaseConfiguration
{
    /// <summary>
    /// Тип полей даты и времени.
    /// </summary>
    public override string DateTimeType => "DATETIME";

    /// <summary>
    /// Указатель использования текущих даты и времени
    /// для полей типа <see cref="DateTimeType"/>.
    /// </summary>
    public override string DateTimeValueCurrent => "CURRENT_TIMESTAMP";
}
