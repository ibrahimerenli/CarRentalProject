using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _CarDal;
        public CarManager(ICarDal carDal)
        {
            _CarDal = carDal;
        }
        public void Add(Car car)
        {
            //Business
            if(car.DailyPrice > 0)
                _CarDal.Add(car);

        }

        public void Delete(Car car)
        {
            //Business
            _CarDal.Delete(car);

        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            return _CarDal.Get(filter); 
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _CarDal.GetAll(filter);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _CarDal.GetAll(p => p.BrandId == id);

        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _CarDal.GetAll(p=> p.ColorId == id);
        }

        public void Update(Car car)
        {
            //Business
            _CarDal.Update(car);
        }
    }
}
