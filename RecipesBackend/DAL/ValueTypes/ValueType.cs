using System.Reflection;
using System.Text;

namespace RecipesBackend.DAL.ValueTypes
{
    public class ValueType<T>
    {
        private static readonly List<PropertyInfo> propertyList;

        static ValueType()
        {
            propertyList = GetOrderedPropertyList(typeof(T));
        }

        public static List<PropertyInfo> GetOrderedPropertyList(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(x => x.Name).ToList();
        }

        public static bool IsOnlyOneObjNull(object first, object second)
        {
            return (first is null || second is null) && first != second;
        }

        public bool Equals(T valueType)
        {
            return Equals(valueType as object);
        }

        public override bool Equals(object obj)
        {
            if (IsOnlyOneObjNull(this, obj))
                return false;

            if (ReferenceEquals(obj, this))
                return true;

            var objProperties = GetOrderedPropertyList(obj.GetType());
            for (var i = 0; i < propertyList.Count(); i++)
            {
                var propValue = propertyList[i].GetValue(this);
                var objPropValue = objProperties[i].GetValue(obj);

                if ((propValue is null) && (objPropValue is null))
                    continue;

                if (IsOnlyOneObjNull(propValue, objPropValue)
                    || propValue.GetHashCode() != objPropValue.GetHashCode()
                    || !propValue.Equals(objPropValue))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            var hash = 100;
            var prime = 16777619;
            foreach (var property in propertyList)
            {
                var bytes = Encoding.UTF8.GetBytes(property.GetValue(this).ToString());
                foreach (var itemByte in bytes)
                {
                    unchecked
                    {
                        hash ^= itemByte;
                        hash *= prime;
                    }
                }
            }
            return hash;
        }

        public override string ToString()
        {
            var sb = new StringBuilder($"{typeof(T).Name}(");

            for (var i = 0; i < propertyList.Count; i++)
            {
                sb.Append($"{propertyList[i].Name}: {propertyList[i].GetValue(this)}");
                if (i + 1 == propertyList.Count)
                    sb.Append($")");
                else
                    sb.Append("; ");
            }
            return sb.ToString();
        }
    }
}
