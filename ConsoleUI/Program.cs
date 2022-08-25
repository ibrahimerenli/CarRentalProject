using System;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
 
namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager _carManager = new CarManager(new EfCarDal());
            var result = _carManager.GetCarsDetails();
            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine($"Marka: {car.BrandName} Renk: {car.ColorName} Model: {car.ModelYear} Günlük Ücret: {car.DailyPrice}");
                }
                Console.WriteLine(result.Message);
            }
            

        }
    }
}