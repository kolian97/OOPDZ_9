// Разработка приложения на C# (семинары)
// Урок 9. Сериализация
// Напишите приложение, конвертирующее произвольный JSON в XML. Используйте JsonDocument.

using System;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        string jsonString = @"
        {
            ""name"": ""John"",
            ""age"": 30,
            ""address"": {
                ""city"": ""New York"",
                ""state"": ""NY""
            },
            ""phones"": [""123-4567"", ""987-6543""]
        }";
        using (JsonDocument doc = JsonDocument.Parse(jsonString))
        {
            XElement xml = ConvertJsonToXml(doc.RootElement, "RootElement");
            Console.WriteLine(xml);
        }
    }
    static XElement ConvertJsonToXml(JsonElement jsonElement, string elementName)
    {
        if (jsonElement.ValueKind == JsonValueKind.Object)
        {
            XElement xmlElement = new XElement(elementName);
            foreach (var property in jsonElement.EnumerateObject())
            {
                xmlElement.Add(ConvertJsonToXml(property.Value, property.Name));
            }
            return xmlElement;
        }
        else if (jsonElement.ValueKind == JsonValueKind.Array)
        {
            XElement xmlArrayElement = new XElement(elementName);
            foreach (var item in jsonElement.EnumerateArray())
            {
                xmlArrayElement.Add(ConvertJsonToXml(item, "Item"));
            }
            return xmlArrayElement;
        }
        else
        {
            return new XElement(elementName, jsonElement.ToString());
        }
    }
}