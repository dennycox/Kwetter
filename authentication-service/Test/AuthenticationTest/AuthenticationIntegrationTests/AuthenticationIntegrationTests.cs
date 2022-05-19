using AuthenticationService.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.WebApplicationFactory;
using Xunit;

namespace Test.AuthenticationTest.AuthenticationIntegrationTests
{
    public class AuthenticationIntegrationTests : IClassFixture<CustomWebApplicationFactory<AuthenticationService.Startup>>
    {
        private HttpClient _client { get; }
        private readonly CustomWebApplicationFactory<AuthenticationService.Startup> _factory;
        private LoginDto _loginDto { get; set; }
        private RegisterDto _registerDto { get; set; }

        public AuthenticationIntegrationTests(CustomWebApplicationFactory<AuthenticationService.Startup> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

            _factory = factory;
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("denny@test.com", "Pa$$w0rd", "Denny")]
        public async Task Test_Authenticate(string email, string password, string expectedName)
        {
            _loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };

            var requestBody = JsonConvert.SerializeObject(_loginDto);
            var postRequest = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/authentication/login", postRequest);
            dynamic responseContents = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            JToken authenticatedUserJSON = responseContents as JObject;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(authenticatedUserJSON["email"], email);
            Assert.Equal(authenticatedUserJSON["name"], expectedName);
            Assert.NotNull(authenticatedUserJSON["token"]);
        }

        [Theory]
        [InlineData("wrong@test.com", "wrongPassword", "Not authorized")]
        public async Task Test_AuthenticateInvalidCredentials(string email, string password, string expectedErrorMessage)
        {
            _loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };

            var requestBody = JsonConvert.SerializeObject(_loginDto);
            var postRequest = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/authentication/login", postRequest);
            dynamic responseContents = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            JToken invalidAuthenticationJSON = responseContents as JObject;

            Assert.Equal(invalidAuthenticationJSON["message"], expectedErrorMessage);
        }

        [Theory]
        [InlineData("test test", "test.test@test,test", "Pa$$w0rd", "A bad request")]
        public async Task Test_RegisterInvalidEmail(string name, string email, string password, string expectedErrorMessage)
        {
            _registerDto = new RegisterDto
            {
                Name = name,
                Email = email,
                Password = password
            };

            var requestBody = JsonConvert.SerializeObject(_registerDto);
            var postRequest = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/authentication/register", postRequest);
            dynamic responseContents = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            JToken invalidRegistrationJSON = responseContents as JObject;

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(invalidRegistrationJSON["message"], expectedErrorMessage);
        }

        [Theory]
        [InlineData("test@test.com", "password", "test", "A bad request")]
        public async Task Test_RegisterInvalidPassword(string email, string password, string name, string expectedErrorMessage)
        {
            _registerDto = new RegisterDto
            {
                Email = email,
                Password = password,
                Name = name,
            };

            var requestBody = JsonConvert.SerializeObject(_registerDto);
            var postRequest = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/authentication/register", postRequest);
            dynamic responseContents = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            JToken invalidRegistrationJSON = responseContents as JObject;

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(invalidRegistrationJSON["message"], expectedErrorMessage);
        }

        [Theory]
        [InlineData("newemail@test.com", "denny@test.com")]
        public async Task Test_EmailExistsCheck(string nonExistingEmail, string existingEmail)
        {
            var response = await _client.GetAsync("api/authentication/emailexists?email=" + nonExistingEmail);
            dynamic responseContents = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            Assert.False(responseContents);

            response = await _client.GetAsync("api/authentication/emailexists?email=" + existingEmail);
            responseContents = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            Assert.True(responseContents);
        }
    }
}
