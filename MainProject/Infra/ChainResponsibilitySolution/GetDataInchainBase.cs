using MainProject.Domain;
using System.Threading.Tasks;

namespace MainProject.Infra.ChainResponsibilitySolution
{
    public abstract class GetDataInchainBase : IGetDataInChain
    {
        protected IGetDataInChain Next;

        public abstract Task<Book> Get(string id);

        public void SetNext(IGetDataInChain next)
        {
            Next = next;
        }
    }
}
