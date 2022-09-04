using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.Infra.ChainResponsibilitySolution
{
    public interface IGetDataInChain
    {
        Task<Book> Get(string id);
    }
}
