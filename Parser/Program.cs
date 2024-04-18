/*
Необходимо разработать консольное приложение, которое  выводит в файл список IP-адресов из файла журнала,
входящих в указанный диапазон с количеством обращений с этого адреса в указанный интервал времени.

yyyy-MM-dd HH:mm:ss

 Все параметры передаются приложению через параметры командной строки:
--file-log — путь к файлу с логами
--file-output — путь к файлу с результатом
--time-start —  нижняя граница временного интервала
--time-end — верхняя граница временного интервала.

--address-start —  нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса
--address-mask — маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. 
В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start

Даты в параметрах задаются в формате dd.MM.yyyy
*/
using System;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;
public class MyClass()
{
    public const string input = @"E:\test\IpParser\Connection_log.txt";
    public const string output = @"E:\test\IpParser\Output.txt";

    public DateTime MaxDate = new(2024, 04, 10);
    public DateTime MinDate = new(1990,01, 01);

    public static void CreateSerchOutputFile()
    {
        FileInfo cfn = new(output);
        
        try
        {
            if (cfn.Exists) { cfn.Delete();}

            using FileStream fs = cfn.Create();
            fs.Close();
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.ToString());
        }
        
    }
    static string GetTime(string prompt)
    {
        string time;
        bool isValid;

        do
        {
            Console.Write(prompt);
            time = Console.ReadLine();
            isValid = DateTime.TryParseExact(time, "yyyy.MM.dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out _);

            if (!isValid)
            {
                Console.Clear();   
                Console.WriteLine("Wrong input, try again. input mask is - yyyy.MM.dd HH:mm:ss");
            }

        } while (!isValid);

        return time;
    }
    public static string StringValidator(string s, string startTime, string endTime)
    {
        try
        {
            int index = s.IndexOf(':');
            string ip = s.Substring(0, index);
            string timeString = s.Substring(index + 1);

            DateTime time = DateTime.ParseExact(timeString, "yyyy.MM.dd HH:mm:ss", null);

            DateTime startRange = DateTime.ParseExact(startTime, "yyyy.MM.dd HH:mm:ss", null);
            DateTime endRange = DateTime.ParseExact(endTime, "yyyy.MM.dd HH:mm:ss", null);

            if (time >= startRange && time <= endRange)
            {
                return(ip);
            }
        }
        catch (Exception Ex)
        {
            Console.WriteLine($"wrong input {Ex.Message}");
        }
        return null;
    }
  
   
    static void Main()
    {
        if (File.Exists(input))
        {
            CreateSerchOutputFile();
            using StreamWriter sw = File.CreateText(output);
            sw.Close();

            using StreamReader reader = new(input);
            using StreamWriter writer = new(output);

            string startTime =GetTime("Enter date to serch from (yyyy.MM.dd HH:mm:ss): ");
            string endTime =GetTime("Enter date to serch to (yyyy.MM.dd HH:mm:ss): ");
            string line;
           
            while ((line = reader.ReadLine()) != null)
            {
                if (StringValidator(line, startTime, endTime) != null) 
                {
                    writer.WriteLine(StringValidator(line, startTime, endTime));
                }
            }
            reader.Close();writer.Close();
        }
        else Console.WriteLine("log file doesn't exist");
    }
}