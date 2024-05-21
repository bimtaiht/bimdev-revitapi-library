
namespace Utility
{
    public static class ConvertUtil
    {
        public static T? Convert<T>(this object element)
        {
            return element is T t ? t : default;
        }
    }
}
