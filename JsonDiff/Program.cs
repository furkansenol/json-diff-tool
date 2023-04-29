// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;


internal class Program
{
    private static void Main(string[] args)
    {
        string json1 = "{\"hello\": \"world\", \"hi\": \"hello\", \"you\": \"me\"}";
        string json2 = "{\"hello\": \"world\", \"hi\": \"helloo\", \"you\": \"me\"}";

        var differentKeys = JsonDiffTool(json1,json2);

        Console.WriteLine("Keys with different values:");
        foreach (var key in differentKeys)
        {
            Console.WriteLine(key);
        }

    }

    public static List<string> JsonDiffTool(string json1, string json2)
    {
        var json1dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json1);
        var json2dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json2);

        var commonKeys = json1dict.Keys.Intersect(json2dict.Keys);

        var differentKeys = new List<string>();

        foreach (var key in commonKeys)
        {
            var value1 = json1dict[key].GetRawText();
            var value2 = json2dict[key].GetRawText();

            if (value1 != value2)
            {
                differentKeys.Add(key);
            }
        }

        return differentKeys;
    }
}