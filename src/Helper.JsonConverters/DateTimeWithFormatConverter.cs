using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Helper.JsonConverters
{
    /// <summary>
    /// Custom DateTime with JSON Serializer / Deserializer Formatting
    /// </summary>
    public class DateTimeWithFormatConverter : DateTimeConverterBase
    {
        private readonly string _format = "dd/MM/yyyy HH:mm:ss";

        public DateTimeWithFormatConverter()
        {

        }

        public DateTimeWithFormatConverter(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Writes value to JSON
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">JSON serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(_format));
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

            return DateTime.Now;
        }
    }
}
