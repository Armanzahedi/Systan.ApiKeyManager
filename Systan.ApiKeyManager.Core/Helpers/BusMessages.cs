using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Helpers
{
    public static class BusMessages
    {
        public const string CREATE_APIKEY = "Create-ApiKey";
        public const string UPDATE_APIKEY = "Update-ApiKey";
        public const string CHANGESTATUSREASON_APIKEY = "ChangeStatusReason-ApiKey";

        public const string CREATE_APIKEYSETTING = "Create-ApiKeySetting";
        public const string UPDATE_APIKEYSETTING = "Update-ApiKeySetting";
        public const string DELETE_APIKEYSETTING = "Delete-ApiKeySetting";
    }
}
