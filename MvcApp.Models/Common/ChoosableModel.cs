namespace MvcApp.Models.Common
{
    public class ChoosableModel<T>
    {
        public T Model { get; set; }
        public bool IsSelected { get; set; }
    }
}
