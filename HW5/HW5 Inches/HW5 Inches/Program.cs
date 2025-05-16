using System;

public class UnitConverter
{
    // 定義一個委派類型，用於指向轉換方法
    public delegate double ConvertDelegate(double value);

    // 將英尺轉換為英吋 (1 英尺 = 12 英吋)
    public static double FeetToInches(double feet)
    {
        return feet * 12;
    }

    // 將碼轉換為英吋 (1 碼 = 3 英尺 = 3 * 12 英吋 = 36 英吋)
    public static double YardsToInches(double yards)
    {
        return yards * 36;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("請輸入要轉換的數值:");
        if (!double.TryParse(Console.ReadLine(), out double value))
        {
            Console.WriteLine("無效的數值輸入。");
            return;
        }

        Console.WriteLine("請輸入要轉換的單位 (feet 或 yards):");
        string unit = Console.ReadLine().ToLower();

        ConvertDelegate converter = null; // 宣告委派變數

        // 根據使用者輸入選擇合適的轉換方法並賦值給委派
        if (unit == "feet")
        {
            converter = FeetToInches;
        }
        else if (unit == "yards")
        {
            converter = YardsToInches;
        }
        else
        {
            Console.WriteLine("無效的單位輸入。請輸入 'feet' 或 'yards'。");
            return;
        }

        // 透過委派動態執行轉換方法
        double result = converter(value);

        Console.WriteLine($"{value} {unit} 等於 {result} 英吋。");
    }
}