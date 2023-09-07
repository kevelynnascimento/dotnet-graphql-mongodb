using System.Text.RegularExpressions;

namespace GraphQL.Config;

public static class EnvBootstrapper
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            var variable = Regex.Replace(parts[0], @"\\|""", string.Empty);
            var value = Regex.Replace(parts[1], @"\\|""", string.Empty);

            Environment.SetEnvironmentVariable(variable, value);
        }
    }

    public static void Load()
    {
        var appRoot = Directory.GetCurrentDirectory();
        var dotEnv = System.IO.Path.Combine(appRoot, "../../.env");

        Load(dotEnv);
    }
}
