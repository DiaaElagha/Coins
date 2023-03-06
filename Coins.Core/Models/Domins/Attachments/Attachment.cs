using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Attachments
{
    public class Attachments
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public byte[] Data { get; set; }
        public DateTimeOffset InsertAt { get; set; }

        public Attachments() {
            Id = Guid.NewGuid().ToString().Replace("-", "");
            InsertAt = DateTimeOffset.Now;
        }
    }
}
