using PD.Domain.Constants;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class FilesService : IFilesService
    { 
        public string GetFilesStorage()
        {
            return Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).ToString(),
                FilesStorageConstants.FilesStoragePath);
        }

        public string DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(GetFilesStorage(), fileName);

            if (File.Exists(filePath))
                throw new NotFoundException("The file was not found.");

            try
            {
                File.Delete(filePath);
            }
            catch (Exception)
            {
                throw new DeletionFailedException();
            }

            return $"The file '{filePath}' was successfully deleted.";
        }

        public FileViewModel LoadFileAsync(string fileName)
        {
            var filePath = Path.Combine(GetFilesStorage(), fileName);

            var extension = Path.GetExtension(fileName);

            if (!File.Exists(filePath))
                throw new NotFoundException($"The file '{filePath}' was not found.");

            FileStream fileStream;
            try
            {
                fileStream = File.OpenRead(filePath);
            }
            catch (Exception)
            {
                throw new FileOpeningException("An error occured while loading the file.");
            }

            return new FileViewModel
            {
                FileStream = fileStream,
                Extension = extension
            };
        }
    }
}
