using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from c in context.Cars
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new CarDetailDto
                             {
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description
                             };

                return result.ToList();
            }
        }
    }
}
