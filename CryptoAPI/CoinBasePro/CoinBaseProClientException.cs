namespace CryptoAPI.CoinBasePro
{
    using System;

    public class CoinBaseProClientException : Exception
    {
        public CoinBaseProClientException()
        { }

        public CoinBaseProClientException(string message)
            : base(message)
        { }

        public CoinBaseProClientException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
