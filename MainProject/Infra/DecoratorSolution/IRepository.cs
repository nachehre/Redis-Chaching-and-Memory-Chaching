using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.Infra.StevenSolution
{
    public interface IRepository
    {
        public Task<Book?> GetAsync(string id);
    }
}
