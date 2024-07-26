using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SupportSecondApp.Utilities;
    public class CloudinaryFileStorage : IFileStorage
    {
        private readonly Cloudinary _cloudinary;
        private readonly long _maxSize;

        public CloudinaryFileStorage(IOptions<CloudinarySetting> config, long maxSize)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
            _maxSize = maxSize;
        }

        public async Task<string> Storage(string container, IFormFile file)
        {
            if (file.Length > _maxSize)
            {
                throw new Exception($"File size exceeds the maximum limit of {_maxSize} bytes.");
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"{Guid.NewGuid()}",
                    Folder = container
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Error uploading file to Cloudinary.");
                }

                return uploadResult.SecureUrl.AbsoluteUri;
            }
        }

        public async Task<IEnumerable<string>> StorageMultiple(string container, IEnumerable<IFormFile> files)
        {
            var urls = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > _maxSize)
                {
                    throw new Exception($"File size exceeds the maximum limit of {_maxSize} bytes.");
                }

                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = $"{Guid.NewGuid()}",
                        Folder = container
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("Error uploading file to Cloudinary.");
                    }

                    urls.Add(uploadResult.SecureUrl.AbsoluteUri);
                }
            }

            return urls;
        }

        public Task Delete(string? route, string container)
        {
            if (string.IsNullOrEmpty(route))
            {
                return Task.CompletedTask;
            }

            var publicId = Path.GetFileNameWithoutExtension(route);

            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.Destroy(deleteParams);

            if (result.Result == "ok")
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new Exception("Error deleting file from Cloudinary.");
            }
        }

        public async Task<string> Edit(string? route, string container, IFormFile file)
        {
            if (!string.IsNullOrEmpty(route))
            {
                await Delete(route, container);
            }
            return await Storage(container, file);
        }
    }
