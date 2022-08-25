using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _CarDal;
        public CarManager(ICarDal carDal)
        {
            _CarDal = carDal;
        }
        public IResult Add(Car car)
        {
            //Business
            if (car.DailyPrice > 0)
            {
                _CarDal.Add(car);
               
            }
            return new SuccessResult(Message.AddedMessage);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll(p => p.BrandId == id), Message.ListedMessage);

        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll(p => p.ColorId == id), Message.ListedMessage);

        }

        public IDataResult<List<CarDetailDto>> GetCarsDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_CarDal.GetCarsDetails().ToList(), Message.ListedMessage);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll().ToList(), Message.ListedMessage);
            
        }
    }
}
