using System;
using System.Collections.Generic;
using Calculator;

namespace Api
{
    public class AccountService : IAccountService
    {
        private readonly ICalculator _calculator;
        private readonly Dictionary<string, double> _accounts = new Dictionary<string, double>();

        public AccountService(ICalculator calculator)
        {
            _calculator = calculator;
        }
        
        public string CreateAccount()
        {
            var accountId = Guid.NewGuid().ToString();

            _accounts[accountId] = 0;

            return accountId;
        }

        public void Credit(string accountId, double amount)
        {
            _accounts[accountId] = _calculator.Sum(_accounts[accountId], amount);
        }

        public void Debit(string accountId, double amount)
        {
            _accounts[accountId] = _calculator.Sum(_accounts[accountId], -amount);
        }

        public double GetBalance(string accountId) => _accounts[accountId];
    }
}