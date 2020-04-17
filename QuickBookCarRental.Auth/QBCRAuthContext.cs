using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBookCarRental.Auth
{
    public class QBCRAuthContext : IdentityDbContext<ApplicationUser>
    {
        public QBCRAuthContext() : base(AuthSettings.ConnectionString)
        {
        }
    }
}
