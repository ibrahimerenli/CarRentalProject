using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            //CarGetDetails();
            //CustomerTest();
            //RentalTest();
            //UserTest();
            //CarTest();

        }

        private static void CarGetDetails()
        {
            CarManager carManager = new CarManager(new EfCarDal(),new ColorManager(new EfColorDal()));
            var result = carManager.GetCarsDetails();
            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine(
                        $"Marka: {car.BrandName} Renk: {car.ColorName} Model: {car.ModelYear} Günlük Ücret: {car.DailyPrice}");
                }

                Console.WriteLine(result.Message);
            }
        }

        private static void CustomerTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            customerManager.Add(new Customer { UserId = 2, CompanyName = "Infinity" });

            var result = customerManager.GetAll();
            foreach (var customer in result.Data)
            {
                Console.WriteLine($"{customer.Id} {customer.UserId} {customer.CompanyName}");
            }
        }
        private static void RentalTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(new Rental { CarId = 3, CustomerId = 2, RentDate = new DateTime(2022, 08, 24) });
            Console.WriteLine(result.Success);
            Console.WriteLine(result.Message);
        }
        private static void UserTest()
        {

            UserManager userManager = new UserManager(new EfUserDal());

            var result = userManager.GetAll();
            foreach (var user in result.Data)
            {
                Console.WriteLine($"{user.Id} {user.FirstName} {user.LastName} {user.Email}");
            }
        }
        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal(), new ColorManager(new EfColorDal()));

            var result = carManager.GetAll();

            if (result.Success)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine($"{car.Id} {car.BrandId} {car.ModelYear} {car.DailyPrice} {car.Description}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}