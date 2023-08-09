using Models.DTO;

namespace Services.Repository.Interface
{
    public interface IURLService
    {
        Task<OperationResult<WidgetConfiguration>> GetURL(long userId,string pageName, string widgetCode, string action);
    }
}