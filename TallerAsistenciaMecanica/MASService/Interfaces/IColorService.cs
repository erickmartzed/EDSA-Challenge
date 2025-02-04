using MASDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASBusiness.Interfaces
{
    public interface IColorService
    {
        Task<Color> GetOrCreateColorAsync(string colorName);
    }
}
