using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        public List<OperationClaim> GetClaims(User user);
    }
}
