﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Studenda.Library.Data.Configuration;
using Studenda.Library.Data.Configuration.Database;
using Studenda.Library.Data.Model;

namespace Studenda.Library.Data.Context;

/// <summary>
/// Сессия работы с внешней базой данных.
/// Характеризуется большим временем доступа к данным.
/// </summary>
public class ApplicationContext : DataContext
{
    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Пока Sqlite в качестве основного хранилища.
        optionsBuilder.UseSqlite("Data Source=storage.db");

        #if DEBUG
            // TODO: Логи в файл или кастом логгер. Поддержка конфигураций.
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        #endif

        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Обработать инициализацию модели.
    /// Используется для дополнительной настройки модели.
    /// </summary>
    /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new SqliteConfiguration();

        // Использование Fluent API.
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new CourseConfiguration(configuration));
        modelBuilder.ApplyConfiguration(new GroupConfiguration(configuration));

        base.OnModelCreating(modelBuilder);
    }
}
