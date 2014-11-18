using MvcApp.BusinessLogic.Repositories.Interfaces;
using MvcApp.Dal.Interfaces;

namespace MvcApp.BusinessLogic.Managers.Base
{
    public class BaseManager
    {
        private readonly IContext _context;
        protected readonly IBaseRepository BaseRepository;

        public BaseManager(IContext context, IBaseRepository baseRepository)
        {
            _context = context;
            BaseRepository = baseRepository;
        }

        public void Commit()
        {
            _context.Commit();
        }
    }
}
