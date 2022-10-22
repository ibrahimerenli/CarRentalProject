using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Message.UserAdded);
           
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Message.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Message.Listed);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p=> p.Id == id), Message.Listed);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Message.UserUpdated);
        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user), Message.Listed);
        }
        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email), Message.Listed);
        }
    }
}
