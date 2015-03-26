namespace WcfServiceTests
{
    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using WcfService.AppStart;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.Users;
    using WcfService.Repositories;

    [TestClass]
    public class UserMethods : BaseTestCreatorAbstract
    {
        private UserRepository repo;



        [TestInitialize()]
        public new virtual void Init()
        {
            base.Init();
        }

        [TestCleanup()]
        public new virtual void Close()
        {
            base.Close();
        }

        [TestMethod]
        public void CreateCorrectUser()
        {
            string userName = "testuserek";
            //in production version password is more complicated
            string userPassword = "testuserek";
            repo = new UserRepository(correctClientVer, context);

            ApplicationUser  loadUser;
            LogInBaseData result;

            
            result = repo.CreateUser(userName, userPassword);
            loadUser = userManager.Find(userName, userPassword);

            Assert.IsNotNull(result);
            Assert.IsFalse(String.IsNullOrWhiteSpace(loadUser.ApplicationLogin));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result.UserKey));
            Assert.AreEqual(result.Login, loadUser.ApplicationLogin);
            Assert.AreEqual(result.UserKey, loadUser.Id);
            Assert.AreEqual(userName, loadUser.UserName);
            
            
        }

        [TestMethod]
        public void FindCorrectUser()
        {
            string userName = DbInitializer.UserName;
            string userPassword = DbInitializer.UserPassword;
            string userAppLogin = DbInitializer.UserAppLogin;
            repo = new UserRepository(correctClientVer, context);
            LogInBaseData result;
            ApplicationUser loadUser;
            String userKeyBefore;
            String userKey;

            userKeyBefore = userManager.Find(userName, userPassword).Id;
            
            result = repo.FindUser(userName, userPassword);
            loadUser = userManager.Find(userName, userPassword);
            userKey = loadUser.Id;

            Assert.IsFalse(String.IsNullOrWhiteSpace(result.UserKey));
            Assert.IsFalse(String.IsNullOrWhiteSpace(result.Login));
            Assert.AreEqual(userKey, result.UserKey);
            Assert.AreEqual(userAppLogin, result.Login);
            Assert.AreEqual(userKeyBefore, userKey);

        }

        [TestMethod]
        public void FindUserWrongName()
        {
            string userName = "nieistniejaca_nazwa";
            string userPassword = DbInitializer.UserPassword;
            repo = new UserRepository(correctClientVer, context);
            LogInBaseData result;
            result = repo.FindUser(userName, userPassword);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindUserWrongPass()
        {
            string userName = DbInitializer.UserName;
            string userPassword = "nieistniejace_haslo";
            LogInBaseData result;
            repo = new UserRepository(correctClientVer, context);
            result = repo.FindUser(userName, userPassword);
            Assert.IsNull(result);
        }
    }
}
