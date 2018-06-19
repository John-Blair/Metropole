using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Metropole.Helpers
{
    public class AddressRepository : IRepository<Address>
    {

        private MetropoleContext _dbContext;


        public AddressRepository(MetropoleContext dbContext)
        {
            _dbContext = dbContext;
       }

        public IQueryable<Address> Query
        {
            get
            {

                return _dbContext.Addresses.AsQueryable();
            }
        }

        public void Add(Address entity)
        {
            _dbContext.Addresses.Add(entity);
            Save();
        }

        public void Delete(Address entity)
        {
            _dbContext.Addresses.Remove(entity);
            Save();
        }

        public List<Address> FetchAll()
        {
            return _dbContext.Addresses.ToList();
        }

        public Address GetById(int id)
        {
            return _dbContext.Addresses.Find(id);
        }

        public void Update(Address entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public IEnumerable<SelectListItem> Addresses()
        {
            var a = this.Query.ToList();


            var allAddresses =  this.Query.OrderBy(m => m.StreetAddress).ThenBy(m => m.FlatNumber).ToSelectList(m => m.Id.ToString(), m => m.FlatNumber + " " + m.StreetAddress);

            return allAddresses.AddPrompt();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
