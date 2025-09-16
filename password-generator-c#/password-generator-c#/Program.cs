﻿using System.Text;

class Program
{
    static void Main()
    {
        string password = CreatePassword(25);
        Console.WriteLine(password);
    }

    static string CreatePassword(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new();
        Random rnd = new();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
}