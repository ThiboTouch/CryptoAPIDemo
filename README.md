# CryptoAPIDemo
##### Development Setup:
This repo demonstrates using third-party Web API client to provide a web service for your application to suit your needs.
The demo is base on Coinbase, a service providing crypto-currency trading. The business case is to work on pulling data for currency pairs available to trade on the Coinbase platform.

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
