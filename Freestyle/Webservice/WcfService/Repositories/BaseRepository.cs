namespace WcfService.Repositories
{
    using System;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Exceptions;

    public abstract class BaseRepository : IDisposable
    {
        protected ApplicationDbContext context { get; private set; }

        //constain user id, only before login / create account can be empty
        protected string userId { get; private set; }
       
        protected BaseRepository(string userKey, ApplicationDbContext context)
        {
            this.context = context;
            userId = getUserId(userKey);
        }

        protected BaseRepository(string userKey) : this(userKey, new ApplicationDbContext()) { }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        //to solve problem between datatime in db and c# (linq)
        //in db less important part of data is losing,
        //so if in c# we want to equal data from c# and from db it can give as wrong value
        protected DateTime CountNow()
        {
            if (now == DateTime.MinValue)
            {
                now = DateTime.Now;
                now = now.AddTicks(-(now.Ticks % TimeSpan.TicksPerSecond));
            }
            return now;
        }

        private DateTime now = DateTime.MinValue;

        //return user id if client version is ok
        //or throw client version exception if not
        private string getUserId(string userKey)
        {
            string versionStr = getVersion(userKey);
            double versionVal;
            Double.TryParse(versionStr, out versionVal);
            if (Global.MinClientVer > versionVal)
            {
                throw new ClientVersionException();
            }
            return userKey.Substring(0, userKey.Length - versionStr.Length - 1);
        }

        private string getVersion(string userKey)
        {
            string result = Global.DefWrongVer.ToString();
            if(userKey != null)
            {
                int startVer = userKey.LastIndexOf(Global.VerTag);
                if (startVer > -1)
                    result = userKey.Substring(startVer + 1);
            }
            return result;
            
        }

    }
}