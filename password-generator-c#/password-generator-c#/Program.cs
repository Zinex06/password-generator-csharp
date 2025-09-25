using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    static void Main()
    {
        var config = new PwConfig();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("--== PASSWORD GENERATOR ==--\n");
            DisplaySettings(config);

            string password = PasswordGenerator.Create(config);
            Console.WriteLine("\nPassword:");
            Console.WriteLine("¨¨¨¨¨¨¨¨¨");
            Console.WriteLine($"{password} \n");

            Console.WriteLine("\nPress any key to generate another password");
            Console.WriteLine("Press [Esc] to exit");
            Console.WriteLine("Press [Spacebar] to open commands (f.ex: len 15, uc false)\n");


            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape) break;
                
            if (key.Key == ConsoleKey.Spacebar)
            {
                Console.Write("command: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    ParseCommand(input, config);
            }
        }
    }

    static void DisplaySettings(PwConfig config)
    {
        Console.WriteLine("Current Settings:");
        Console.WriteLine("¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨");
        Console.WriteLine($"[len] Length:   {config.Length}");
        Console.WriteLine($"[lc] Lowercase: {config.RequireLower}");
        Console.WriteLine($"[uc] Uppercase: {config.RequireUpper}");
        Console.WriteLine($"[num] Numbers:  {config.RequireNumber}");
    }

    static void ParseCommand(string input, PwConfig config)
    {
        string[] parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            var cmd = part.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (cmd.Length != 2) continue;

            string key = cmd[0].ToLower();
            string value = cmd[1].ToLower();

            switch (key)
            {
                case "len":
                    if (int.TryParse(value, out int len))
                    {
                        config.Length = Math.Max(1, len);
                    }
                    break;

                case "lc":
                    config.RequireLower = value == "true" || value == "y";
                    break;

                case "uc":
                    config.RequireUpper = value == "true" || value == "y";
                    break;

                case "num":
                    config.RequireNumber = value == "true" || value == "y";
                    break;
            }
        }
    }
}

public class PwConfig
{
    public int Length { get; set; } = 20;
    public bool RequireLower { get; set; } = true;
    public bool RequireUpper { get; set; } = true;
    public bool RequireNumber { get; set; } = true;
}

public static class PasswordGenerator
{
    public static string Create(PwConfig config)
    {
        var valid = new StringBuilder();
        if (config.RequireLower) valid.Append("abcdefghijklmnopqrstuvwxyz");
        if (config.RequireUpper) valid.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (config.RequireNumber) valid.Append("1234567890");

        if (valid.Length == 0)
            throw new InvalidOperationException("No character sets selected!");

        var res = new StringBuilder();
        var rnd = new Random();

        for (int i = 0; i < config.Length; i++)
            res.Append(valid[rnd.Next(valid.Length)]);

        return res.ToString();
    }
}