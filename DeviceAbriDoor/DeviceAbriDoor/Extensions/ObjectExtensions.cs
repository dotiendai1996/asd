using Newtonsoft.Json;
using System.Linq.Expressions;

namespace System
{
    public static class ObjectExtensions
    {
        public static string GetPropertyName<TClass>(Expression<Func<TClass, object>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                body = (expression.Body as UnaryExpression).Operand as MemberExpression;
            return body.Member.Name;
        }

        public static object GetValueByObject(this object obj, string propertyName)
        {
            if (obj == null) return string.Empty;
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static T ToObjectModel<T>(this object obj)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            string objectJson = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(objectJson, settings);
        }
    }
}
