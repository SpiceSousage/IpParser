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
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;
public class MyClass()
{
    public const string input = @"E:\test\IpParser\Connection_log.txt";
    public const string output = @"E:\test\IpParser\Output.txt";

    public DateTime MaxDate = new(04,10,2024 );
    public DateTime MinDate = new(01,01,1990);

    public static void CreateSerchOutputFile()
    {
        FileInfo cfn = new(output);
        using StreamReader file = new(input);

        try
        {
            if (cfn.Exists) { cfn.Delete();}

            using FileStream fs = cfn.Create();

            // Add some text to file
            //Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
            //fs.Write(title, 0, title.Length);
            //byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");
            //fs.Write(author, 0, author.Length);
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.ToString());
        }
    }
    public static DateTime UserInputValidator() 
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (DateTime.TryParse(input, out DateTime num))
            {
                return num;
            }
        }
    }
    public static DateTime CheckIfUserInputIsDateTime() 
    {
         DateTime date = UserInputValidator(); 
         return date;
    }
    public static bool StringValidator(string s)
    {
        DateTime date = new();
       
        try
        {
            date = DateTime.Parse(s);
            return true;
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.ToString());
        }
        return false;
    }
  
    static void Main()
    {
        if (File.Exists(input))
        {
            using StreamReader file = new(input);
            
            Console.WriteLine("enter date to serch from");
            DateTime FirstDate = CheckIfUserInputIsDateTime();
            
            Console.WriteLine("enter date to serch to");
            DateTime LastDate = CheckIfUserInputIsDateTime();

            Console.WriteLine(FirstDate);
            Console.WriteLine(LastDate);

            
            string ln;
            
            
            //int result = DateTime.Compare(FirstDate, LastDate);

            CreateSerchOutputFile();
            using StreamWriter sw = File.CreateText(output);

            while ((ln= file.ReadLine()) != null)
            {
                //trim

                if (StringValidator(ln)) {
                    Console.WriteLine(ln) ; 
                }
            }
            file.Close();

            //Console.WriteLine($"File has {counter} lines.");
        }
        else Console.WriteLine("log file doesn't exist");
    }
}