using System.IO;

namespace DocumentGenerator.Infrastructure;

/// <summary>Мінімальний журнал: logs/application.log. Ніколи не кидає винятків.</summary>
public static class AppLog
{
    private static readonly object Gate = new();

    public static void Write(string message, Exception? ex = null)
    {
        try
        {
            lock (Gate)
            {
                var text = ex is null
                    ? $"{DateTime.UtcNow:O} | {message}\n\n"
                    : $"{DateTime.UtcNow:O} | {message}\n{ex}\n\n";
                File.AppendAllText(Path.Combine(AppPaths.Logs, "application.log"), text);
            }
        }
        catch { /* журнал не повинен ламати програму */ }
    }
}
