using Common;
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL
{    
    public class CellularModel : DbContext
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<MostCalled> MostCalled { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ServiceAgent> ServiceAgents { get; set; }
        public DbSet<SMS> SMS { get; set; }

        public CellularModel()
            : base("name=CellularModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.        
    }
}