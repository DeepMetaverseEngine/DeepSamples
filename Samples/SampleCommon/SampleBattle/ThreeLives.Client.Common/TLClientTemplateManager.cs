using DeepCore;
using DeepMMO.Client;
using DeepMMO.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TLClient
{
    public class TLClientTemplateManager : RPGClientTemplateManager
    {
        public static string BATTLE_DATA_ROOT;
        public static string SERVER_LIST_URL;

        public TLClientTemplateManager()
        {
            base.LoadServerList(SERVER_LIST_URL);
        }

        //-----------------------------------------------------------------------------------------------------
        #region Roles

        public override RoleTemplateData[] AllRoleTemplates
        {
            get { return null; }
        }
        public override RoleTemplateData GetRoleTemplate(int id, byte gender)
        {
            return null;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------


    }

}
