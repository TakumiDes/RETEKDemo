using RETEKDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RETEKDemo.DataProvider
{
    public interface IMessageDataProvider
    {
        Task<bool> IsCorrectParentId(int parentId);

        Task<IEnumerable<Messages>> GetMessages(int perentId);

        Task<Messages> AddMessage(Messages message); 
    }
}
