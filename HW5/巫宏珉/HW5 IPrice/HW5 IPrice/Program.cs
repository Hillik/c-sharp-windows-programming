using System;

// 定義 IPrice 介面
public interface IPrice
{
    void DisplayPrice();
}

// 定義 Vehicle 基底類別
public class Vehicle
{
    // 可以加入 Vehicle 的通用屬性和方法
    public string Model { get; set; }
    public int Year { get; set; }

    public Vehicle(string model, int year)
    {
        Model = model;
        Year = year;
    }

    // 可以在基底類別中加入虛擬方法供衍生類別覆寫
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"型號 (Model): {Model}, 年份 (Year): {Year}");
    }
}

// 定義 Car 類別，繼承自 Vehicle 並實現 IPrice 介面
public class Car : Vehicle, IPrice
{
    public decimal Price { get; set; }

    public Car(string model, int year, decimal price) : base(model, year)
    {
        Price = price;
    }

    // 實現 IPrice 介面的 DisplayPrice 方法
    public void DisplayPrice()
    {
        Console.WriteLine($"價格 (Price): {Price:C}"); // 使用貨幣格式顯示價格
    }

    // 可以覆寫基底類別的 DisplayInfo 方法
    public override void DisplayInfo()
    {
        base.DisplayInfo(); // 呼叫基底類別的 DisplayInfo
        Console.WriteLine($"類型 (Type): Car");
    }
}

// 其他類別如 Truck, Motorcycle, SportsCar, Jeep 可以類似地衍生自 Vehicle 或 Car
// 例如:
// public class Truck : Vehicle { ... }
// public class SportsCar : Car { ... }


// 範例用法
public class Program
{
    public static void Main(string[] args)
    {
        // 建立一個 Car 物件
        Car myCar = new Car("房車", 2023, 30000.00m);

        // 呼叫 DisplayInfo 方法 (來自 Vehicle 類別)
        myCar.DisplayInfo();

        // 呼叫 DisplayPrice 方法 (來自 IPrice 介面的實現)
        myCar.DisplayPrice();

        // 也可以透過 IPrice 介面引用來呼叫 DisplayPrice
        IPrice priceableVehicle = myCar;
        priceableVehicle.DisplayPrice();
    }
}