using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.Infra.Steven_Solution
{
    public interface IRepository
    {
        public ValueTask<Book?> GetAsync(string id);
    }
}
