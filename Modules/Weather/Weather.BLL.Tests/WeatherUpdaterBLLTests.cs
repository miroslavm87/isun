using Microsoft.Extensions.Logging;
using Moq;
using Weather.Infrastructure.BLLs;
using Weather.Infrastructure.Contracts;
using Weather.Infrastructure.DAL;

namespace Weather.BLL.Tests
{
    public class Tests
    {
        private WeatherUpdaterBLL _weatherUpdaterBLL;
        private Mock<IWeatherGetterBLL> _weatherGetterBLLMock;
        private Mock<IWeatherDAL> _weatherDALMock;
        private Mock<ILogger<WeatherUpdaterBLL>> _loggerMock;
        private int addEntityCalls = 0;
        private int saveChangesCalls = 0;

        [SetUp]
        public void Setup()
        {
            _weatherGetterBLLMock = new();
            _weatherGetterBLLMock
                .Setup(x => x.GetWeatherForCity(It.IsAny<string>()).Result)
                .Callback((string city) => { }).Returns(() =>
                new WeatherContract()
                {
                    City = "Vilnius",
                    Precipitation = 2,
                    Summary = "Good",
                    Temperature = 22,
                    WindSpeed = 10,
                }
                );


            _weatherDALMock = new Mock<IWeatherDAL>();
            _weatherDALMock.Setup(weatherDal => weatherDal.AddEntityAsync(It.IsAny<Weather.Infrastructure.Models.Weather>()))
                .Callback(() => addEntityCalls++)
                .Returns(Task.CompletedTask);
            _weatherDALMock.Setup(weatherDal => weatherDal.SaveChangesAsync())
                .Callback(() => saveChangesCalls++)
                .Returns(Task.CompletedTask);

            _loggerMock = new Mock<ILogger<WeatherUpdaterBLL>>();
            _weatherUpdaterBLL = new(_weatherDALMock.Object, _weatherGetterBLLMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task MultipleApiCallsOneSaveTest()
        {
            addEntityCalls = 0;
            saveChangesCalls = 0;
            await _weatherUpdaterBLL.UpdateWeathersForCities(new string[] { "Vilnius" });
            Assert.IsTrue(addEntityCalls == 1);
            Assert.IsTrue(saveChangesCalls == 1);

            addEntityCalls = 0;
            saveChangesCalls = 0;
            await _weatherUpdaterBLL.UpdateWeathersForCities(new string[] { "Vilnius", "Kaunas", "Klaipeda" });
            Assert.IsTrue(addEntityCalls == 3);
            Assert.IsTrue(saveChangesCalls == 1);
        }
    }
}