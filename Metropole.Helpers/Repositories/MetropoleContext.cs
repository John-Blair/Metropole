using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metropole.Helpers
    {


    public class MetropoleContext : DbContext

    {

        public MetropoleContext() : base("DefaultConnection")
        { }
        public DbSet<Address> Addresses { get; set; }
    }
}
