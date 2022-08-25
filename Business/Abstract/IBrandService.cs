using Core.DataAccess;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBrandService : IEntityRepository<Brand>
    {
    }
}
