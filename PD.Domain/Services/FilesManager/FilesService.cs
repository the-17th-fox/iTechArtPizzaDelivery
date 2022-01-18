using Microsoft.AspNetCore.Mvc;
using PD.Domain.Constants;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class FilesService : IFilesService
    {
        public async Task<string> DeleteFileAsync(string fileName)
        {
            var path = Path.Combine(FilesConstants.RootFilesPath, fileName);

            if (File.Exists(path))
                throw new NotFoundException("The file was not found.");

            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                throw new DeletionFailedException();
            }

            return $"The file '{path}' was successfully deleted.";
        }

        public async Task<FileViewModel> LoadFileAsync(string fileName)
        {
            var path = Path.Combine(FilesConstants.RootFilesPath, fileName);

            if (!File.Exists(path))
                throw new NotFoundException("The file was not found.");

            try
            {
                var fileStream = File.Open(path, FileMode.Open);
            }
            catch (Exception)
            {
                throw new FileOpeningException("An error occured while loading the file.");
            }

            return new FileViewModel
            {
                FilePath = path,
                FileType = FilesConstants.ImageFileType
            };
        }
    }
}
