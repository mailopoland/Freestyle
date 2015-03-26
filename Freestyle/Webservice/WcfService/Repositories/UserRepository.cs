namespace WcfService.Repositories
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.Users;

    public class UserRepository : BaseRepository
    {
        public UserRepository(string userKey, ApplicationDbContext context) : base(userKey, context) { }

        public UserRepository(string userKey) : base(userKey) { }

        //NOT COMPL TESTED (NOTI BOOLS)
        public LogInBaseData FindUser(string userName, string password)
        {
            LogInBaseData result = null;
            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
            {
                ApplicationUser user = userManager.Find(userName, password);
                result = getLogInBaseData(user);
            }
            return result;
        }

        //NOT COMPL TESTED (NOTI BOOLS, THE SAME PASSWORD)
        public LogInBaseData CreateUser(string userName, string password)
        {
            LogInBaseData result = null;
            if (codePass(userName) == password) { 
                var randomCreator = new Random();
                int randomSeed = randomCreator.Next(1000);
                string loginName;
                bool isSucceeded;
                ApplicationUser user;
                using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    loginName = new StringBuilder("Anonim").Append(context.Users.Count()).Append(randomSeed).ToString();
                    user = new ApplicationUser()
                    {
                        UserName = userName,
                        ApplicationLogin = loginName,
                        NotiFreq = Global.DefaultNotiFreq,
                        NoAcceptNoti = false,
                        NoRespNoti = false
                    };
                    var createResult = userManager.Create(user, password);
                    isSucceeded = createResult.Succeeded;
                }

                if (isSucceeded)
                {
                    result = getLogInBaseData(user);
                }
            }
            return result;
        }

        //UNTESTED
        //return: 0 = unknown error, 1 = success, 2 = error login exsist
        public int ChangeUserLogin(string newLogin)
        {
            int result = 0;
            //u.Id != userKey -> allow user change his login on the same login
            if (context.Users.Where(u => u.ApplicationLogin == newLogin && u.Id != userId).Any())
                result = 2;
            else
            {
                ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.ApplicationLogin = newLogin;
                    context.SaveChanges();
                    result = 1;
                }
            }
            return result;
        }

        //UNTESTED
        public bool ChangeShowEmail(bool show)
        {
            bool result = false;
            ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                if (show)
                    user.Email = user.UserName;
                else
                    user.Email = null;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        //UNTESTED
        public bool ChangeNotiResp(bool noNoti)
        {
            bool result = false;
            ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                user.NoRespNoti = noNoti;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        //UNTESTED
        public bool ChangeNotiAccept(bool noNoti)
        {
            bool result = false;
            ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                user.NoAcceptNoti = noNoti;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        //UNTESTED
        public bool ChangeNotiFreq(int value)
        {
            bool result = false;
            ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                user.NotiFreq = value;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        private LogInBaseData getLogInBaseData(ApplicationUser user)
        {
            LogInBaseData result = null;
            if (user != null)
            {
                result = new LogInBaseData()
                {
                    UserKey = user.Id,
                    Email = user.Email,
                    Login = user.ApplicationLogin,
                    NotiFreq = user.NotiFreq
                };
                //for send less data check cond
                if (user.NoAcceptNoti)
                    result.NoAcceptNoti = user.NoAcceptNoti;
                if (user.NoRespNoti)
                    result.NoRespNoti = user.NoRespNoti;
            }
            return result;
        }
        //have to be the same in all clients!
        private string codePass(string input)
        {
            //in production version here is counted one way hash base on input string
            //remove from here for security reason 

            return input;
        }

        //unused change for allow to only show or not, may be it can be use in future
        /*
        //UNTESTED
        //return: 0 = unknown error, 1 = success, 2 = error email exsist
        public int ChangeUserEmail(string newEmail)
        {
            int result = 0;
            if (String.IsNullOrEmpty(newEmail))
            {
                newEmail = null;
            }
            else{
                newEmail = newEmail.ToLower();
                if(!new Validator().IsValidEmail(newEmail))
                    return 0;
                else if (context.Users.Where(u => u.Email == newEmail && u.Id != userId).Any())
                    return 2;
            }

            ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                user.Email = newEmail;
                context.SaveChanges();
                result = 1;
            }
            return result;
        }
        */
        
        
    }
}