using MASDataAccess.Models;

namespace MASBusiness.Interfaces
{
    public interface IUtilsService
    {
        bool IsValidLicensePlate(string input);
    }
}