using System.Threading.Tasks;
using Windows.Storage;

namespace IconExtract.Services.Interfaces
{
    public interface IDialogService
    {
        string[] FileExtensions { get; set; }

        Task<IStorageFile> ShowDialog();
    }
}
