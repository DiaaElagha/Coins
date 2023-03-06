using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Core.Constants
{
    public enum ResponseMessages
    {
        [Description("Entity is already exist in system")]
        EntityExist,
        [Description("Entity not exist in system")]
        EntityNotExist,
        [Description("Some fields are required")]
        ModelStateInValid,
        [Description("Operation successfully")]
        Operation,
        [Description("Added successfully")]
        CREATE,
        [Description("Updated successfully")]
        UPDATE,
        [Description("Get Data successfully")]
        READ,
        [Description("Deleted successfully")]
        DELETE,
        [Description("Something went wrong")]
        FAILED,
    }
}
