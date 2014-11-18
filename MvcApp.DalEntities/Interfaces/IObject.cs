using System;

namespace MvcApp.DalEntities.Interfaces
{
    public interface IObject
    {
        int Id { get; set; }
        Byte[] EntityVersion { set; get; }
    }
}
