using System.Collections.Generic;
using MvcApp.DalEntities.Entities;

namespace MvcApp.BusinessLogic.Managers.Interfaces
{
    public interface ITeacherManager
    {
        List<Teacher> FindByFio(string fio);
    }
}
