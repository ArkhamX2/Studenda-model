namespace Studenda.Library;

/// <summary>
/// Класс констант для конфигурации базы данных.
/// </summary>
internal static class DataConstant
{
    /// <summary>
    /// Конфигурации, характерные для SQLite.
    /// </summary>
    public static class SQLite
    {
        /// <summary>
        /// Тип полей даты и времени.
        /// </summary>
        public const string DateTimeType = "TEXT";

        /// <summary>
        /// Указатель использования текущих даты и времени
        /// для полей типа <see cref="DateTimeType"/>.
        /// </summary>
        public const string DateTimeCurrent = "CURRENT_TIMESTAMP";
    }
}
