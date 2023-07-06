namespace Portal.Repository.Inbox
{
    public interface IInboxRepository
    {
        string GetInboxData(); 
    }
    public class InboxRepository : IInboxRepository
    {
        public string GetInboxData()
        {
            return "This is valid Inbox data";
        }
    }
}