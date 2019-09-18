using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Helper.JsonConverters
{

    /// <summary>
    /// Custom DateTime with JSON Serializer / Deserializer Formatting
    /// </summary>
    public class DateTimeWithFormatConverter : IsoDateTimeConverter
    {
        private readonly string _format;

        public DateTimeWithFormatConverter()
        {
            _format = "dd/MM/yyyy HH:mm:ss";
            DateTimeFormat = _format;
            Culture = CultureInfo.CreateSpecificCulture("pt-BR");
        }

        public DateTimeWithFormatConverter(string format)
        {
            _format = format;
            DateTimeFormat = format;
        }

        /// <summary>
        /// Writes value to JSON
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">JSON serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateTime dateTime))
            {
                throw new InvalidCastException("Campo não é um DateTime.");
            }

            writer.WriteValue(dateTime.ToString(_format));
        }

        /// <summary>
        /// Reads value from JSON
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Target type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON serialized</param>
        /// <returns>Deserialized DateTime</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            var s = reader.Value.ToString();
            DateTime result;
            if (DateTime.TryParseExact(s, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) return result;

            return DateTime.MinValue;
        }
    }
}
