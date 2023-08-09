using Services.Repository.Interface;

namespace Portal.Repository.Inbox
{
   
    public class InboxService : IInboxRepository
    {
        public string GetInboxData()
        {
            return "This is valid Inbox data";
        }
    }
}