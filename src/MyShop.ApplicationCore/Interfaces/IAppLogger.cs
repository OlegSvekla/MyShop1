namespace MyShop.ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogInformation(string messege, params object[] args);

        void LogWarning(string messege, params object[] args);

        void LogError(Exception exception, string? messege, params object[] args);


    }
}
