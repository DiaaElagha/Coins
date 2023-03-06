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
    public class StoresBranchsAttachmentsRepository : Repository<StoresBranchsAttachments>, IStoresBranchsAttachmentsRepository
    {
        public StoresBranchsAttachmentsRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(StoresBranchsAttachments attachment)
        {
            Context.Update(attachment);
        }

        public async Task<List<StoresBranchsAttachments>> AddRangeAttachments(Guid userId, int storeBranchId, List<Attachments> attachments)
        {
            List<StoresBranchsAttachments> attachmentsList = new List<StoresBranchsAttachments>();
            foreach (var item in attachments)
            {
                attachmentsList.Add(new StoresBranchsAttachments
                {
                    AttachmentsId = item.Id,
                    StoreBranchId = storeBranchId,
                    CreateByUserId = userId
                });
            }
            await Context.StoresBranchsAttachments.AddRangeAsync(attachmentsList);
            return attachmentsList;
        }

    }
}
