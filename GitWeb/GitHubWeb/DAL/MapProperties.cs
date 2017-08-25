using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class MapProperties
    {
        public static bool CopyProperties(this object source, object dest)
        {
            Type typedest = dest.GetType();
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;
                try
                {
                    PropertyInfo other = typedest.GetProperty(property.Name);
                    if ((other != null) && (other.CanWrite))
                        other.SetValue(dest, property.GetValue(source, null), null);
                }
                catch
                {
                    return false;
                }

            }
            return true;

        }
    }
}
