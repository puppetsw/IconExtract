using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using IconExtract.Models;
using IconExtract.Services.Implementation;

namespace IconExtract.Services.Interfaces
{
    public interface IExportService
    {
        Task<(ExportFormat, IStorageFolder)> ShowDialog();

        Task ExportTo(IEnumerable<IconObject> icons, ExportFormat format, IStorageFolder destinationFolder);


    }
}
