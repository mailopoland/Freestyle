namespace WcfServiceTests
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using WcfService;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;

    public abstract class BaseTestCreatorAbstract : IDisposable
    {

        protected String correctClientVer { get{ return Global.VerTag + Global.MinClientVer.ToString(); } }
        protected String wrongClientVer { get{ return Global.VerTag + Global.DefWrongVer.ToString(); } }

        protected DateTime nowDate
        {
            get
            {
                DateTime nowDateValue = DateTime.Now;
                //trick to solve problem of unprecission read data by ef from db
                nowDateValue = nowDateValue.AddTicks(-(nowDateValue.Ticks % TimeSpan.TicksPerSecond));
                return nowDateValue;
            }
        }

        protected ApplicationDbContext context { get;  private set; }
        private ApplicationDbContext contextPoorValue;
        protected ApplicationDbContext contextPoor
        {
            get
            {
                if (contextPoorValue == null)
                {
                    contextPoorValue = new ApplicationDbContext(nameOfContextPoor);

                    if (contextPoorValue.Database.Exists())
                        contextPoorValue.Database.Delete();
                    contextPoorValue.Database.Create();
                    WcfService.AppStart.DbInitializerPoor.Seed(contextPoorValue);
                }
                return contextPoorValue;
            }
        }
        protected UserManager<ApplicationUser> userManager { get; private set; }
        protected String nameOfContext
        {
            get
            {
                return "RhymeWcfContextTest";
            }
        }
        protected String nameOfContextPoor
        {
            get
            {
                return "RhymeWcfContextTestPoor";
            }
        }
        [TestInitialize()]
        public virtual void Init()
        {
            if (context == null)
            {
                context = new ApplicationDbContext(nameOfContext);

                if (context.Database.Exists())
                    context.Database.Delete();
                context.Database.Create();
                context.Database.Initialize(true);
                //to init db:
                context.Rhymes.First();
                userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            }

            
        }

        [TestCleanup()]
        public virtual void Close()
        {
            userManager.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (contextPoorValue != null)
                    contextPoorValue.Dispose();
                if (context != null)
                    context.Dispose();
            }
        }
    }
}
