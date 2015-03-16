using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using WalzExplorer.Controls.TreeView.ViewModel;
using WalzExplorer.Controls.RHSTabs;
using System.Data.Entity.Core.Metadata.Edm;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;

namespace WalzExplorer.Database
{
    public partial class ServicesEntities : DbContext
    {
        public ServicesEntities(bool test)
            : base("name=ServicesEntities")
        {
            // Add the password programatically (should not store in config file
            var originalConnectionString = ConfigurationManager.ConnectionStrings["ServicesEntities"].ConnectionString;
            var entityBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            var factory = DbProviderFactories.GetFactory(entityBuilder.Provider);
            var providerBuilder = factory.CreateConnectionStringBuilder();
            providerBuilder.ConnectionString = entityBuilder.ProviderConnectionString;
            providerBuilder.Add("Password", "1JUDQlPsYzHm6s");
            entityBuilder.ProviderConnectionString = providerBuilder.ToString();
            //this.Database.Connection.ConnectionString = entityBuilder.ToString();
            this.Database.Connection.ConnectionString = providerBuilder.ToString();
        }

        public DateTime ServerDateTime()
        {
            DateTime? sdt = this.spServices_ServerDatetime().FirstOrDefault();
            if (!sdt.HasValue)
                throw new Exception("Cannot get server date time");
            else
                return sdt.Value;
        }

    }

}
    