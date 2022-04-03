using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseApp.Data
{
    public class MenuInfo
    {
        public string MenuType { get; set; }
        public int MenuId { get; set; }
        public int ParentMenuId { get; set; }
        public string PageName { get; set; }
        public string MenuName { get; set; }
        public string IconName { get; set; }
    }
}
