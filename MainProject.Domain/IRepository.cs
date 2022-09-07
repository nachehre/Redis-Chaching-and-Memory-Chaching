using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.Domain
{
    public interface IRepository
    {
        public Task<Book?> GetAsync(string id);
    }
}
