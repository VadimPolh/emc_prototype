using System.Collections;
using MvcApp.DalEntities.Interfaces;

namespace MvcApp.Dal.DomainExtensions
{
    public static class Extension
    {
        public static bool IsNew<T>(this T @this) where T : IObject
        {
            return @this.Id == 0;
        }

        public static bool ByteArrayCompare(this byte[] a1, byte[] a2)
        {
            IStructuralEquatable eqa1 = a1;
            return eqa1.Equals(a2, StructuralComparisons.StructuralEqualityComparer);
        }

        public static bool Compare<T>(this T x, T y) where T : IObject
        {
            return (x == null && y == null) || (x != null && y != null && (x.Id == y.Id));
        }
    }
}
