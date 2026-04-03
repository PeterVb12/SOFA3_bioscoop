namespace SOFA_bioscoop.Domain
{
    public interface INotificationService
    {
        void Send(Person recipient, string message);
    }
}
