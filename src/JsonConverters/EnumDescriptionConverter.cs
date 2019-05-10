using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace JsonConverters
{
    public class EnumDescriptionConverter : JsonConverter
    {

        public string Variavaledeclaradaenuncausada;

        public void TesteCodeCoverage()
        {
            try
            {
                var i = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool canConvert = CanConvert(objectType);
            if (!canConvert)
                throw new TypeLoadException("Type not allowed. Use this converter only in enumerations.");

            FieldInfo fieldInfo  = objectType.GetFields()
                               .FirstOrDefault(f => ((JsonPropertyAttribute)f.GetCustomAttribute(typeof(JsonPropertyAttribute), false))
                               ?.PropertyName == reader.Value.ToString());

            if (fieldInfo == null)
                throw new ArgumentNullException(objectType.Name, "JsonProperty is not defined.");

            return fieldInfo.GetValue(fieldInfo);
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool canConvert = CanConvert(value.GetType());
            if (!canConvert)
                throw new TypeLoadException("Type not allowed. Use this converter only in enumerations.");

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            JsonPropertyAttribute attribute = fieldInfo.GetCustomAttribute(typeof(JsonPropertyAttribute), false) as JsonPropertyAttribute;
            string description = attribute?.PropertyName != null ? attribute.PropertyName : value.ToString();

            writer.WriteValue(description);
        }
    }
}
