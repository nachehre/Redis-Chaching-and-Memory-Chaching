using System.Threading.Tasks;

namespace MainProject.Infra.Steven_Solution.Services
{
    public class RedisRepository : IRepository
    {
        public ValueTask<Domain.Book> GetAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
