using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorris.Services
{
    public interface IRepository<T>
    {
        Task<IEnumerable<string>> GetCategories();
        Task<T> GetFromCategory(string category);
        Task<IEnumerable<T>> GetFromSearch(string query, string filter = null);

    }
}
