namespace Dal;

using DO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security;
using System;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

static class XMLTools
{
    const string s_xml_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_xml_dir))
            Directory.CreateDirectory(s_xml_dir);
    }

    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;
    public static int? ToNullableInt(this XElement element, string name)
    {
        return int.TryParse(element?.Element(name)?.Value, out var result) ? (int?)result : null;
    }
    #endregion
    public static void SetProjectDates(DateTime? Date, string elemName)
    {
        try
        {
            if (Date.HasValue)
            {
                XElement root = XMLTools.LoadListFromXMLElement("data-config");
                if (root != null)
                {
                    XElement element = root.Element(elemName);
                    if (element != null)
                    {
                        element.SetValue(Date.Value.ToString("MM/dd/yyyy"));
                        XMLTools.SaveListToXMLElement(root, "data-config");
                        Console.WriteLine($"Successfully set {elemName} to {Date.Value.ToString("MM/dd/yyyy")}");
                        return; // Exit the method after successful execution
                    }

                    Console.WriteLine($"Element {elemName} not found in the XML file.");
                }
                else
                {
                    Console.WriteLine("Failed to load XML file.");
                }
            }
            else
            {
                Console.WriteLine("Date parameter is null.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while setting {elemName}: {ex.Message}");
        }
    }

    public static DateTime? GetProjectStartDate()
    {
        try
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");

            // Find the StartDate element
            string startDateStr = root.Element("StartDate")?.Value;

            // Convert the StartDate string to a DateTime object
            if (DateTime.TryParse(startDateStr, out DateTime startDate))
            {
                return startDate;
            }
            else
            {
                // Log or handle the case where the StartDate is not in a valid format
                Console.WriteLine("The start date in the XML configuration is not in a valid format.");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Log or handle any exceptions that occur during processing
            Console.WriteLine($"An error occurred while retrieving the project start date: {ex.Message}");
            return null;
        }
    }

    public static DateTime? GetProjectEndDate()
    {
        try
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");

            // Find the EndDate element
            string endDateStr = root.Element("EndDate")?.Value;

            // Convert the EndDate string to a DateTime object
            if (DateTime.TryParse(endDateStr, out DateTime endDate))
            {
                return endDate;
            }
            else
            {
                // Log or handle the case where the EndDate is not in a valid format
                Console.WriteLine("The end date in the XML configuration is not in a valid format.");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Log or handle any exceptions that occur during processing
            Console.WriteLine($"An error occurred while retrieving the project end date: {ex.Message}");
            return null;
        }
    }






    #region XmlConfig
    public static int GetAndIncreaseNextId(string data_config_xml, string elemName, bool reset)
    {

        
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);

        // Attempt to get the value of NextId, or use a default value of 1 if it doesn't exist
        int nextId = root.ToNullableInt(elemName) ?? 1;

        if (reset == true)
        {
            nextId = 0; 
        }

        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return nextId;
    } 
    #endregion

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
        }
    }
    #endregion
}