using Dapper;
using KnowledgeBaseApp.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseApp.Services
{
    public class MenuService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public MenuService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<MenuInfo>> GetMenuData()
        {
            IEnumerable<MenuInfo> menuInfos;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"select 'category' MenuType, Id MenuId, null ParentMenuId, null PageName, name MenuName, IconName from Categories
                                        union all
                                        select 'subcategory' MenuType, Id MenuId, CategoryId ParentMenuId, null PageName, name MenuName, IconName from SubCategories
                                        union all
                                        select 'article' MenuType, Id MenuId, SubCategoryId ParentMenuId, Title PageName, Title MenuName, null IconName from Articles";

                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                try
                {
                    menuInfos = await conn.QueryAsync<MenuInfo>(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
            return menuInfos;
        }
    }
}
