using System.Collections.Generic;
using NetBackendBootstrap.Enum;
using NetBackendBootstrap.Utils;
using Newtonsoft.Json.Linq;
using Xunit;

namespace NetBackendBootstrap.test
{
    public class MockType
    {
        public string Foo { get; set; }

        public string[] Arr { get; set; }

    }

    public class UtilsTest
    {
        public static string ExpectedFoo = "bar";

        public static string[] ExpectedArr = new string[] { "a", "b", "c" };

        public static JObject ExpectedJsonResult = new JObject(new JProperty("Foo", ExpectedFoo), new JProperty("Arr", new JArray(ExpectedArr)));

        [Fact]
        public void JsonExtensions_Typed_FromList()
        {
            var expectedResult = new JArray(ExpectedJsonResult);
            IList<MockType> mockTypedObject = new List<MockType> {
                new MockType()
                    {
                        Foo = ExpectedFoo,
                        Arr = ExpectedArr
                    }
            };

            var result = JsonExtensions.FromList<MockType>(mockTypedObject);
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void JsonExtensions_Annonymous_FromList()
        {
            var expectedResult = new JArray(ExpectedJsonResult);
            var mockAnonnymousObjectList = new List<dynamic> { new { Foo = ExpectedFoo, Arr = ExpectedArr } };

            var result = JsonExtensions.FromList<object>(mockAnonnymousObjectList);
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void JsonExtensions_Typed_FromObject()
        {
            var mockTypedObject = new MockType()
            {
                Foo = ExpectedFoo,
                Arr = ExpectedArr
            };

            var result = JsonExtensions.FromObject<MockType>(mockTypedObject);
            Assert.Equal(result, ExpectedJsonResult);
        }

        [Fact]
        public void JsonExtensions_Annonymous_From_Object()
        {
            var mockAnonnymousObject = new { Foo = ExpectedFoo, Arr = ExpectedArr };

            var result = JsonExtensions.FromObject<object>(mockAnonnymousObject);
            Assert.Equal(result, ExpectedJsonResult);
        }

        [Theory]
        [InlineData(ResponseStatus.BadRequest)]
        [InlineData(ResponseStatus.NotFound)]
        [InlineData(ResponseStatus.OK)]
        [InlineData(ResponseStatus.InternalServerError)]
        [InlineData(ResponseStatus.Unauthorized)]
        [InlineData(ResponseStatus.Created)]
        [InlineData(ResponseStatus.NoContent)]
        public void EnumExtensions_ExtractDescription(ResponseStatus message)
        {
            var expectedBadRequest = "Bad request";
            var expectedSuccess = "Request success";
            var expectedUnauthorized = "Unauthorized";
            var expectedError = "An unexpected error has occurred";
            string expectedNoContent = "";
            var expectedNotFound = "Not found";
            var expectedCreated = "Successfully created";

            var result = EnumExtensions.ExtractDescription<ResponseStatus>(message);
            switch (message)
            {
                case ResponseStatus.BadRequest:
                    {
                        Assert.Equal(result, expectedBadRequest);
                        break;
                    }

                case ResponseStatus.OK:
                    {
                        Assert.Equal(result, expectedSuccess);
                        break;
                    }

                case ResponseStatus.InternalServerError:
                    {
                        Assert.Equal(result, expectedError);
                        break;
                    }

                case ResponseStatus.Unauthorized:
                    {
                        Assert.Equal(result, expectedUnauthorized);
                        break;
                    }

                case ResponseStatus.NoContent:
                    {
                        Assert.Equal(result, expectedNoContent);
                        break;
                    }

                case ResponseStatus.NotFound:
                    {
                        Assert.Equal(result, expectedNotFound);
                        break;
                    }

                case ResponseStatus.Created:
                    {
                        Assert.Equal(result, expectedCreated);
                        break;
                    }
            }
        }

    }
}
