namespace FSMS_Hybrid.Services
{
    public static class DebugLogger
    {
        public static List<string> Logs { get; } = new();
        public static event Action? OnLogAdded;

        public static void Log(string message)
        {
            var log = $"[{DateTime.Now:HH:mm:ss}] {message}";
            Logs.Add(log);
            Console.WriteLine(log);
            OnLogAdded?.Invoke();
        }
    }
}
