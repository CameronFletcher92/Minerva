namespace MinervaService.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MinervaApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MinervaService.MinervaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MinervaService.MinervaContext context)
        {
            int numEquips = 100;
            List<string> equipNames = new List<string>() { "PUMP", "WELL", "TRUCK", "CONVEYER" };
            int timeSeed = 80;
            int daysAgo = 100;

            List<int> equipNameCounts = new List<int>();
            for (int i = 0; i < equipNames.Count; i++)
            {
                equipNameCounts.Add(1);
            }

            Random rand = new Random();
            List<Equipment> equips = new List<Equipment>();
            List<DowntimeEvent> downtimes;
            int cursor;
            for (int i = 0; i < numEquips; i++)
            {
                Equipment equip = new Equipment();
                cursor = rand.Next(equipNames.Count);

                equip.Code = equipNames[cursor] + equipNameCounts[cursor].ToString();
                equip.Description = String.Format("Demo {0} number {1}", equipNames[cursor], equipNameCounts[cursor]);
                equipNameCounts[cursor]++;

                downtimes = new List<DowntimeEvent>();
                DateTime start = DateTime.Now.AddDays(-daysAgo);
                while (start < DateTime.Now)
                {
                    DowntimeEvent dt = new DowntimeEvent();
                    dt.Start = start;
                    dt.End = start.AddDays(rand.NextDouble() * timeSeed);
                    start = dt.End.Value;

                    downtimes.Add(dt);
                }

                equip.DowntimeEvents = downtimes;
                equips.Add(equip);
            }

            context.Equipments.AddOrUpdate(
                e => e.Id,
                equips.ToArray()
            );
        }
    }
}
