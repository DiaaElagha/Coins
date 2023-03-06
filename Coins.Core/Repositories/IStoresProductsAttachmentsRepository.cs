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
    public interface IStoresProductsAttachmentsRepository : IRepository<StoresProductsAttachments>
    {
        Task<List<StoresProductsAttachments>> AddRangeAttachments(Guid userId, int storeProductId, List<Attachments> attachments);
        void Update(StoresProductsAttachments storeProductAttachment);

    }
}
