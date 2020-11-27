using System.Collections.Generic;
using NetBackendBootstrap.Enum;
using NetBackendBootstrap.Model;
using Newtonsoft.Json.Linq;
using Xunit;

namespace NetBackendBootstrap.test
{
    public class ResponseTest
    {
        [Fact]
        public void Response_StatusJArrayParam_Constructor()
        {
            var expectedStatus = ResponseStatus.OK;
            var expectedBody = "{\"results\":[{\"foo\":\"bar\"}]}";

            var mockWithJArrayParam = new Response(new JArray(new JObject(new JProperty("foo", "bar"))));

            Assert.Equal(mockWithJArrayParam.Status, expectedStatus);
            Assert.Equal(mockWithJArrayParam.Body, expectedBody);
        }

        [Fact]
        public void Response_StatusResponseStatusParam_Constructor()
        {
            var expectedStatus = ResponseStatus.BadRequest;
            var expectedBody = "{\"message\":\"Bad request\"}";

            var mockWithResponseStatusParam = new Response(expectedStatus);

            Assert.Equal(mockWithResponseStatusParam.Status, expectedStatus);
            Assert.Equal(mockWithResponseStatusParam.Body, expectedBody);
        }

        [Fact]
        public void Response_StatusParamMessageParam_Constructor()
        {
            var expectedStatus = ResponseStatus.BadRequest;
            var expectedBody = "{\"message\":\"Something specific\"}";

            var mockWithResponseStatusParam = new Response(expectedStatus, "Something specific");

            Assert.Equal(mockWithResponseStatusParam.Status, expectedStatus);
            Assert.Equal(mockWithResponseStatusParam.Body, expectedBody);
        }

        [Theory]
        [InlineData(ResponseStatus.BadRequest)]
        [InlineData(ResponseStatus.NotFound)]
        [InlineData(ResponseStatus.OK)]
        [InlineData(ResponseStatus.InternalServerError)]
        [InlineData(ResponseStatus.Unauthorized)]
        [InlineData(ResponseStatus.Created)]
        [InlineData(ResponseStatus.NoContent)]
        public void Response_StatusParamOnly_Constructor(ResponseStatus status)
        {
            var expectedStatus = status;
            var expectedBadRequest = "{\"message\":\"Bad request\"}";
            var expectedNoContent = "{\"message\":\"\"}";
            var expectedOk = "{\"message\":\"Request success\"}";
            var expectedCreated = "{\"message\":\"Successfully created\"}";
            var expectedUnauthorized = "{\"message\":\"Unauthorized\"}";
            var expectedError = "{\"message\":\"An unexpected error has occurred\"}";
            var expectedNotFound = "{\"message\":\"Not found\"}";

            var mockWithResponseStatusParam = new Response(expectedStatus);

            Assert.Equal(mockWithResponseStatusParam.Status, expectedStatus);

            switch (status)
            {
                case ResponseStatus.BadRequest:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedBadRequest);
                        break;
                    }

                case ResponseStatus.OK:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedOk);
                        break;
                    }

                case ResponseStatus.InternalServerError:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedError);
                        break;
                    }

                case ResponseStatus.Unauthorized:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedUnauthorized);
                        break;
                    }

                case ResponseStatus.NoContent:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedNoContent);
                        break;
                    }

                case ResponseStatus.NotFound:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedNotFound);
                        break;
                    }

                case ResponseStatus.Created:
                    {
                        Assert.Equal(mockWithResponseStatusParam.Body, expectedCreated);
                        break;
                    }
            }
        }

        [Fact]
        public void Response_FromList()
        {
            var mockList = new List<object> { new { foo = "bar" } };
            var expectedStatus = ResponseStatus.OK;
            var expectedBody = "{\"results\":[{\"foo\":\"bar\"}]}";

            var mockFromList = Response.FromList(mockList);

            Assert.Equal(mockFromList.Status, expectedStatus);
            Assert.Equal(mockFromList.Body, expectedBody);
        }
    }
}
