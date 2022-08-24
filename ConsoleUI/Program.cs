using System;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager _carManager = new CarManager(new EfCarDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            
            var cars = _carManager.GetAll();
            var colors = colorManager.GetAll();
            var brands = brandManager.GetAll();

            foreach (var car in cars)
            {
                Console.WriteLine($"Marka: {brandManager.Get(p => p.Id == car.BrandId).Name} Renk: {colorManager.Get(p => p.Id == car.ColorId).Name} Model: {car.ModelYear}");
            }

       


        }
    }
}
