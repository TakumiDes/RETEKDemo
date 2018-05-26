using RETEKDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RETEKDemo.DataProvider
{
    public interface IMessageDataProvider
    {
        Task<IEnumerable<Messages>> GetMessages(int perentId);

        Task<Messages> AddMessage(Messages message); 
    }
}
