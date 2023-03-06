using Coins.Core.Models.Domins.Attachments;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;

using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class StoresProductsAttachmentsRepository : Repository<StoresProductsAttachments>, IStoresProductsAttachmentsRepository
    {
        public StoresProductsAttachmentsRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(StoresProductsAttachments attachment)
        {
            Context.Update(attachment);
        }

        public async Task<List<StoresProductsAttachments>> AddRangeAttachments(Guid userId, int storeProductId, List<Attachments> attachments)
        {
            List<StoresProductsAttachments> attachmentsList = new List<StoresProductsAttachments>();
            foreach (var item in attachments)
            {
                attachmentsList.Add(new StoresProductsAttachments
                {
                    AttachmentsId = item.Id,
                    StoreProductId = storeProductId,
                    CreateByUserId = userId
                });
            }
            await Context.StoresProductsAttachments.AddRangeAsync(attachmentsList);
            return attachmentsList;
        }

    }
}
