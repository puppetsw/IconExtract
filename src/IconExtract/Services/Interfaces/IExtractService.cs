using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using IconExtract.Models;

namespace IconExtract.Services.Interfaces
{
    public interface IExtractService
    {
        Task<IList<IconObject>> ExtractIconsFromDll(IStorageFile file);
    }
}