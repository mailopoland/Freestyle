namespace WcfServiceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using WcfService;
    using WcfService.MainHelpers;

    [TestClass]
    public class OtherTests
    {
        [TestMethod]
        public void CorrectGlobalValues()
        {
            Assert.IsTrue(Global.MinMsgAmount % 2 == 0 && Global.MaxMsgAmount % 2 == 0, "MinMsgAmount and MaxMsgAmount have to be even numbers");
            Assert.IsFalse(Global.MinMsgAmount >= Global.MaxMsgAmount, "MinMsgAmount have to be < than MaxMsgAmount");
        }

        [TestMethod]
        public void TextCheckerBase()
        {
            String pre = "Chuj *kurWA,  bĄdź,kurwa co bądź*, pIerdolĘ, " + '\n' + "k u r w a";
            String post = TextChecker.CheckText(pre);
            String expexted = "C***j *k***A,  bĄdź,k***a co bądź*, p***Ę, k u r w a";
            for(int i = 0; i < expexted.Length ; i++){
                if(expexted[i] != post[i])
                    Assert.Fail("Fail at char:" + expexted[i] + ", index:" + i );
            }
            //for sure that test is ok
            Assert.AreEqual(expexted,  post);
        }

        /*use it to create swear list (remove duplicate and sort via sortset)
        [TestMethod]
        public void ZPrepareSwearList()
        {
            string result = "";
            foreach (string el in Swear.PolishSwears)
                result += "\"" + el + "\",\n";
        }
        */

       
    }
}
