using Coins.Core;
using Coins.Core.Models.Domins.Attachments;
using Coins.Core.Services;
using Coins.Core.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Services
{
    public class StorageService
    {
        private readonly IMongoCollection<Attachments> _attachments;

        public StorageService()
        {
            var client = new MongoClient(MongoDBSettings.ConnectionString);
            var database = client.GetDatabase(MongoDBSettings.DatabaseName);
            _attachments = database.GetCollection<Attachments>(MongoDBSettings.AttachmentsCollectionName);
        }

        public async Task<List<Attachments>> GetFiles(List<string> ids)
        {
            return await _attachments.Find(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Attachments>> GetAll()
        {
            return await _attachments.Find(x => true).ToListAsync();
        }

        public async Task<Attachments> GetFile(string id) =>
            await _attachments.Find<Attachments>(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Attachments> InsertFile(Attachments attachment)
        {
            attachment = addMetaData(attachment);
            await _attachments.InsertOneAsync(attachment);
            return attachment;
        }

        public async Task<List<Attachments>> InsertFiles(List<Attachments> attachments)
        {
            attachments = attachments.Select(item => addMetaData(item)).ToList();
            await _attachments.InsertManyAsync(attachments);
            return attachments;
        }

        public async Task RemoveFile(string id) => await _attachments.DeleteOneAsync(x => x.Id == id);

        public async Task<Attachments> UploadFile(IFormFile file)
        {
            if (file is not null)
            {
                if (file.Length > 0)
                {
                    var attachmentResult = await InsertFile(MapFileToAttachment(file));
                    return attachmentResult;
                }

            }
            return null;
        }

        public async Task<List<Attachments>> UploadFiles(List<IFormFile> files)
        {
            if (files is not null)
            {
                if (files.Count() > 0)
                {
                    var attachments = files.Select(file => MapFileToAttachment(file)).ToList();
                    var attachmentsResult = await InsertFiles(attachments);
                    return attachmentsResult;
                }
            }
            return null;
        }

        public Attachments MapFileToAttachment(IFormFile file)
        {
            var fileName = file.FileName;
            byte[] bytes = null;
            using (var fileStream = file.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return new Attachments()
            {
                Name = fileName,
                Data = bytes,
                Extension = Path.GetExtension(fileName),
            };
        }

        private Attachments addMetaData(Attachments attachment)
        {
            var provider = new FileExtensionContentTypeProvider();
            var contentType = provider.TryGetContentType(attachment.Name, out var type)
                ? type
                : "application/octet-stream";
            attachment.MimeType = contentType;
            return attachment;
        }

    }
}
