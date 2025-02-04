using MASDataAccess.Models;

namespace MASBusiness.Interfaces
{
    public interface IModelService
    {
        Task<Model> GetOrCreateModelAsync(string modelName, string brandName);
    }
}