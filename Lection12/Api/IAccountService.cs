namespace Api
{
    public interface IAccountService
    {
        string CreateAccount();
        void Credit(string accountId, double amount);
        void Debit(string accountId, double amount);
        double GetBalance(string accountId);
    }
}