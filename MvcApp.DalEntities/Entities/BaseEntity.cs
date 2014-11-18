using MvcApp.DalEntities.Interfaces;

namespace MvcApp.DalEntities.Entities
{
    public class BaseEntity : IObject
    {
        #region IObject implementation

        public int Id { get; set; }

        public byte[] EntityVersion { get; set; }

        #endregion
    }
}
