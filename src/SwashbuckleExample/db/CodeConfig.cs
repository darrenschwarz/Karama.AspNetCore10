using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace SwashbuckleExample.db
{
    public class CodeConfig : DbConfiguration
    {
        public CodeConfig()
        {
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        }
    }
}