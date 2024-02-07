using System.Collections;
using System.Reflection;
namespace BlImplementation;

internal static class Tools
{
    public static string ToStringProperty<T>(this T t, string str = " ")
    {
        IEnumerable<PropertyInfo> propertyInfos = t!.GetType().GetProperties();

        foreach (PropertyInfo item in propertyInfos)
        {
            object value = item.GetValue(t, null)!;

            if (value is not null)
            {
                if (value is IEnumerable objects and not string)
                {
                    str += "\n" + "items " + ":\n";
                    foreach (var obj in objects)
                        str += obj.ToStringProperty(str);
                }
                else
                    str += "\n" + item.Name + ": " + value;
            }
        }
        return str;// Return the final string
    }


}
