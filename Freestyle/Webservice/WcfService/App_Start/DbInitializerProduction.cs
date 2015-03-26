//*** -> means that data from production version was hided for security reason

namespace WcfService.AppStart
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;

    public class DbInitializerProduction : System.Data.Entity.CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        public static string UserId;
        public static string UserName = "mailo.poland@gmail.com";
        public static string UserPassword = "***";
        public static string UserAppLogin = "M@ilo";
        public static string UserEmail = UserName;
        public static int UserNotiFreq = 1;

        public static string UserReplyId;
        public static string UserNameReply = "a@a.com";
        public static string UserPasswordReply = "***";
        public static string UserAppLoginReply = "toMek";

        public static string UserReply2Id;
        public static string UserNameReply2 = "b@a.com";
        public static string UserPasswordReply2 = "***";
        public static string UserAppLoginReply2 = "Nienażarty wiewiór";

        public static string UserReply3Id;
        public static string UserNameReply3 = "c@a.com";
        public static string UserPasswordReply3 = "***";
        public static string UserAppLoginReply3 = "Zosia";

        public static string UserNoDataId;
        public static string UserNameNoData = "d@a.com";
        public static string UserPassNoData = "***";
        public static string UserAppLoginNoData = "Kozak";

        protected override void Seed(ApplicationDbContext context)
        {
            DateTime myDate = DateTime.Now;
            //trick to solve problem of unprecission read data by ef from db
            myDate = myDate.AddTicks(-(myDate.Ticks % TimeSpan.TicksPerSecond));

            var user = new ApplicationUser();
            user.UserName = DbInitializerProduction.UserName;
            user.ApplicationLogin = DbInitializerProduction.UserAppLogin;
            user.Email = DbInitializerProduction.UserEmail;
            user.NotiFreq = DbInitializerProduction.UserNotiFreq;

            var userReply = new ApplicationUser();
            userReply.UserName = DbInitializerProduction.UserNameReply;
            userReply.ApplicationLogin = DbInitializerProduction.UserAppLoginReply;

            var userReply2 = new ApplicationUser();
            userReply2.UserName = DbInitializerProduction.UserNameReply2;
            userReply2.ApplicationLogin = DbInitializerProduction.UserAppLoginReply2;

            var userReply3 = new ApplicationUser();
            userReply3.UserName = DbInitializerProduction.UserNameReply3;
            userReply3.ApplicationLogin = DbInitializerProduction.UserAppLoginReply3;

            var userNoData = new ApplicationUser();
            userNoData.UserName = DbInitializerProduction.UserNameNoData;
            userNoData.ApplicationLogin = DbInitializerProduction.UserAppLoginNoData;

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userResult = userManager.Create(user, DbInitializerProduction.UserPassword);
            var userResultReply = userManager.Create(userReply, DbInitializerProduction.UserPasswordReply);
            var userResultReply2 = userManager.Create(userReply2, DbInitializerProduction.UserPasswordReply2);
            var userResultReply3 = userManager.Create(userReply3, DbInitializerProduction.UserPasswordReply3);
            var userResultNoData = userManager.Create(userNoData, DbInitializerProduction.UserPassNoData);
        }
    }
}