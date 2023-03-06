using Coins.Core.Models.Domins.Attachments;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Repositories
{
    public interface IStoresBranchsAttachmentsRepository : IRepository<StoresBranchsAttachments>
    {
        Task<List<StoresBranchsAttachments>> AddRangeAttachments(Guid userId, int storeBranchId, List<Attachments> attachments);
        void Update(StoresBranchsAttachments StoreBranchAttachment);

    }
}
