using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IColorService _colorService;

        public CarManager(ICarDal carDal, IColorService colorService)
        {
            _carDal = carDal;
            _colorService = colorService;
        }

        [SecuredOperation("car.add, admin")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator), Priority = 1)]
        public IResult Add(Car car)
        {
            //Business
            var result = BusinessRules.Run(CheckIfCarCountOfColorCorrect(car.ColorId), CheckIfColorLimitExceeded(15));
            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult(Message.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Message.CarDeleted);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Message.CarUpdated);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id), Message.Listed);

        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id), Message.Listed);

        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == id), Message.Listed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails().ToList(), Message.Listed);
        }

        [PerformanceAspect(5)]
        //[SecuredOperation("car.list,admin")]
        //[LogAspect(typeof(FileLogger))]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Car>> GetAll()
        {
            //System.Threading.Thread.Sleep(5000);
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().ToList(), Message.Listed);

        }

        private IResult CheckIfCarCountOfColorCorrect(int colorId)
        {
            var count = _carDal.GetAll(c => c.ColorId == colorId).Count;
            if (count >= 10)
            {
                return new ErrorResult(Message.LimitExceeded);
            }

            return new SuccessResult();
        }
        private IResult CheckIfColorLimitExceeded(int count)
        {

            var result = _colorService.GetAll();
            if (result.Data.Count >= count)
            {
                return new ErrorResult(Message.LimitExceeded);
            }

            return new SuccessResult();
        }
    }
}
