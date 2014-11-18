using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcApp.BusinessLogic.Managers.Base;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.BusinessLogic.Repositories.Interfaces;
using MvcApp.Dal.Interfaces;
using MvcApp.DalEntities.Entities;

namespace MvcApp.BusinessLogic.Managers.Implementations
{
    public class TeacherManager : BaseManager, ITeacherManager
    {
        public TeacherManager(IContext context, IBaseRepository baseRepository) : base(context, baseRepository)
        {
        }

        public List<Teacher> FindByFio(string fio)
        {
            var query = BaseRepository.QueryWithNoTracking<Teacher>();
            if (!String.IsNullOrEmpty(fio))
            {
                query = query.Where(x => x.Fio.ToLower().Contains(fio.ToLower()));
            }
            return query.ToList();
        }
    }
}
