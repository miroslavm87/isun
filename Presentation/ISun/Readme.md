# isun.exe Service
Receives weather information from thrird party API and saves new weather information into database. Default fetching period is 15seconds, but you can change it in appsettings.config

## Service execution pattern
<code>
isun.exe --cities City1, City2, ... , CityN
</code>

## Service execution example
<code>
isun.exe --cities Vilnius, Kaunas
</code>

## Production Environment - appsettings.config
Service use weather api which needs credencials. Please 
fill username and password fields before using this service.

<code>
  "WeatherIntegrationOptions": {
    "Host": "https://weather-api.isun.ch/",
    "Username": "[PLEASE FILL]",
    "Password": "[PLEASE FILL]"
  },
</code>

## Developer Environment
### appsettings.config
Don't place passwords or usernames to appsettings.config. Please use Secrets Manager.