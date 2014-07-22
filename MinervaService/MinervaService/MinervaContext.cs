using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MinervaApi.Models;

namespace MinervaService
{
    public class MinervaContext : DbContext
    {
        //public MinervaContext() : base("MinervaAzure") { }

        public MinervaContext() : base() { }

        public MinervaContext(string connection) : base(connection) { }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<DowntimeEvent> DowntimeEvents { get; set; }
    }
}