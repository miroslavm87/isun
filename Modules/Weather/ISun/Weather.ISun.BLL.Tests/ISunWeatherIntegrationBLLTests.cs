using RichardSzalay.MockHttp;
using Weather.ISun.BLL.Options;

namespace Weather.ISun.BLL.Tests
{
    public class ISunWeatherIntegrationBLLTests
    {
        private const string host = "https://localhost/";
        private const string token = "6d5d2f70fae64d203b5bc7045cdf4922b2278817d2dc2981bf66a7f92c7124ac";
        private ISunWeatherIntegrationBLL _iSunWeatherIntegrationBLL;
        private HttpClient _httpClient;
        private ISunWeatherIntegrationOptions _sunWeatherIntegrationOptions;

        [SetUp]
        public void Setup()
        {
            _sunWeatherIntegrationOptions = new ISunWeatherIntegrationOptions() { Host = host };

            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("*/api/authorize")
                    .Respond("application/json",
                    "{'token' : '" + token + "'}");

            mockHttp.When("*/api/weathers/*")
                    .Respond("application/json",
                    "{\r\n    \"city\": \"Vilnius\",\r\n    \"temperature\": -9,\r\n    \"precipitation\": 81,\r\n    \"windSpeed\": 6,\r\n    \"summary\": \"Cool\"\r\n}");

            _httpClient = new HttpClient(mockHttp);
            _iSunWeatherIntegrationBLL = new ISunWeatherIntegrationBLL(_httpClient, _sunWeatherIntegrationOptions);
        }

        [Test]
        public async Task GetAuthorizationToken()
        {
            var receivedToken = await _iSunWeatherIntegrationBLL.GetAuthorizationToken();
            Assert.That(receivedToken, Is.EqualTo(token));
        }

        [Test]
        public async Task GetWeather()
        {
            var weather = await _iSunWeatherIntegrationBLL.GetWeatherForCity("Vilnius");
            Assert.IsNotEmpty(weather.City);
            Assert.IsNotEmpty(weather.Summary);
            Assert.IsNotNull(weather.Temperature);
            Assert.IsNotNull(weather.Precipitation);
            Assert.IsNotNull(weather.WindSpeed);
        }
    }
}