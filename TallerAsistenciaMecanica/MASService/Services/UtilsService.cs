using MASBusiness.Interfaces;
using MASDataAccess.Data;
using MASDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MASBusiness.Services
{
    public class UtilsService : IUtilsService
    {
        public bool IsValidLicensePlate(string input)
        {
            string pattern = @"^[A-Z]{2}\d{2}[A-Z]{2}$|^[A-Z]{3}\d{3}$";
            return Regex.IsMatch(input, pattern);
        }
    }

}
