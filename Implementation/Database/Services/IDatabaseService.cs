using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Services
{
    public interface IDatabaseService<TObject, TIdentifierType>
    {
        List<TObject> GetAll();
        TObject Get(TIdentifierType id);
        Task<TIdentifierType> Create(TObject userModel);
        Task<bool> Update(TObject userModel);
        Task<bool> Remove(TIdentifierType id);
    }
}