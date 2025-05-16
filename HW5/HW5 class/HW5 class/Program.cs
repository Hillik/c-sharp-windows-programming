using System;
using System.Linq; // 用於 MinElement 方法 (可選)

public class Calculator
{
    // Cube 方法多載 (計算平方)
    public static int Cube(int number)
    {
        return number * number;
    }

    public static double Cube(double number)
    {
        return number * number;
    }

    // MinElement 方法多載 (尋找最小值)
    public static int MinElement(int a, int b, int c)
    {
        return Math.Min(a, Math.Min(b, c));
        // 或者使用 Linq: return new[] { a, b, c }.Min(); // 需要 using System.Linq;
    }

    public static int MinElement(int a, int b, int c, int d)
    {
        return Math.Min(a, Math.Min(b, Math.Min(c, d)));
        // 或者使用 Linq: return new[] { a, b, c, d }.Min(); // 需要 using System.Linq;
    }

    // 範例用法
    public static void Main(string[] args)
    {
        Console.WriteLine($"Cube(5): {Cube(5)}"); // 呼叫 int 多載
        Console.WriteLine($"Cube(5.5): {Cube(5.5)}"); // 呼叫 double 多載

        Console.WriteLine($"MinElement(10, 20, 5): {MinElement(10, 20, 5)}"); // 呼叫 3 個 int 參數的多載
        Console.WriteLine($"MinElement(10, 20, 5, 15): {MinElement(10, 20, 5, 15)}"); // 呼叫 4 個 int 參數的多載
    }
}