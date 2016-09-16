using DevOpsPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DevOpsPortal.Managers
{
    public static class ServerManager
    {
        private static ServerDBContext db = new ServerDBContext();
        private static List<Server> servers = new List<Server>();
        private static DateTime LastUpdate = DateTime.Now;
        private static TimeSpan offSet = new TimeSpan(-5, 0, 0);

        public static List<Server> GetServers()
        {
            if(servers.Count == 0 || LastUpdate == DateTime.Now.AddMinutes(-5))
            {
                servers = (from m in db.MasterServerInventories
                           orderby m.ComputerName
                           select m).ToList();
            }
            return servers;
        }

        public static Server GetServer(int id)
        {
            Server server = db.MasterServerInventories.Find(id);

            return server;
        }

        public static void CreateServer(Server server)
        {
            db.MasterServerInventories.Add(server);
            db.SaveChanges();
        }

        public static void UpdateServer(Server server)
        {
            db.Entry(server).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeleteServer(int id)
        {
            Server server = GetServer(id);
            db.MasterServerInventories.Remove(server);
            db.SaveChanges();
        }
    }
}