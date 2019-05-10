using Helper.JsonConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace UnitTestProjectCore
{
    [TestClass]
    public class EnumDescriptionConverterTest
    {
        [TestMethod]
        public void EnumDescription_ProcessedMessage()
        {
            try
            {
                TestConverter testConverter = new TestConverter { Id = Guid.NewGuid(), TypeMessage = TypeMessage.PROCESSED };

                string json = JsonConvert.SerializeObject(testConverter);

                Assert.IsTrue(json.Contains("Processed Message"));

                TestConverter testConverterDeserialize = JsonConvert.DeserializeObject<TestConverter>(json);

                Assert.IsTrue(testConverterDeserialize.TypeMessage == TypeMessage.PROCESSED);
            }
            catch
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void EnumDescription_WriteJson_IsNotEnum()
        {
            try
            {
                TestConverterWithoutEnum testConverter = new TestConverterWithoutEnum { Id = Guid.NewGuid(), TypeMessage = TypeMessage.PROCESSED.ToString() };
                string json = JsonConvert.SerializeObject(testConverter);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex.Message == "Type not allowed. Use this converter only in enumerations.");
            }

        }

        [TestMethod]
        public void EnumDescription_ReadJson_JsonProperty_NotDefined()
        {
            try
            {
                TestConverterJsonPropertyNotDefined testConverter = new TestConverterJsonPropertyNotDefined { Id = Guid.NewGuid(), TypeMessage = TypeMessageJsonPropertyNotDefined.UNPROCESSED };
                string json = JsonConvert.SerializeObject(testConverter);
                Assert.IsTrue(json.Contains("UNPROCESSED"));
                TestConverter testConverterDeserialize = JsonConvert.DeserializeObject<TestConverter>(json);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message == "JsonProperty is not defined.\r\nParameter name: TypeMessage");
            }

        }
    }

    public class TestConverter
    {
        public Guid Id { get; set; }
        public TypeMessage TypeMessage { get; set; }
    }

    [JsonConverter(typeof(EnumDescriptionConverter))]
    public class TestConverterWithoutEnum
    {
        public Guid Id { get; set; }
        public string TypeMessage { get; set; }
    }

    public class TestConverterJsonPropertyNotDefined
    {
        public Guid Id { get; set; }
        public TypeMessageJsonPropertyNotDefined TypeMessage { get; set; }
    }

    [JsonConverter(typeof(EnumDescriptionConverter))]
    public enum TypeMessageJsonPropertyNotDefined
    {
        UNPROCESSED = 0,

        [JsonProperty("Processed Message")]
        PROCESSED = 1,

        [JsonProperty("Error in Message processing")]
        ERROR = 2
    }

    [JsonConverter(typeof(EnumDescriptionConverter))]
    public enum TypeMessage
    {
        [JsonProperty("Unprocessed Message")]
        UNPROCESSED = 0,

        [JsonProperty("Processed Message")]
        PROCESSED = 1,

        [JsonProperty("Error in Message processing")]
        ERROR = 2
    }
}
