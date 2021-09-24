# CryptoAPIDemo
##### Development Setup:
This repo demonstrates using a third-party Web API client to provide a web service for your application.
The demo is based on Coinbase (https://www.coinbase.com/), a service that provides crypto-currency trading. 

The use case is for working on the currency pair data available for trading.

Follow the instructions using the dotnet core sdk command tools.

1. Ensure you run database migrations to set up the identity database for authentication and authorization:
    ___
    dotnet ef database update
    ___
2. Run the following command to start the server app:
    ___
    dotnet watch run
    ___
3. Once the server is running navigate to https://localhost:5001/swagger to familiarise yourself with the API

The repo also includes unit tests for testing API query results.
