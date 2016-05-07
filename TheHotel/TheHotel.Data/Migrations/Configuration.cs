using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Data.DbFactory;

namespace TheHotel.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<HotelContext>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        #endregion

    }
}
