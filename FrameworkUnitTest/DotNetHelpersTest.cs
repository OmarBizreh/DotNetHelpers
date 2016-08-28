using System.Collections.Generic;
using DotNetHelpers.Helpers.Extensions;
using DotNetHelpers.Service.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FrameworkUnitTest
{
    [TestClass]
    public class DotNetHelpersTest
    {
        private string EncryptionPass { get { return "someRandomString"; } }
        private Token TestToken { get; set; }

        [TestMethod]
        public void Test_CreateToken()
        {
            var tokenArgs = new Dictionary<string, string>();
            tokenArgs.Add("Key1", "Value1");
            tokenArgs.Add("Key2", "Value2");

            this.TestToken = new Token(tokenArgs, this.EncryptionPass);

            Assert.IsNotNull(this.TestToken);
            Assert.IsInstanceOfType(this.TestToken, typeof(Token));
            System.Console.WriteLine(string.Format("Generated Token: {0}\nNumber of Args: {1}", this.TestToken.AuthenticationToken, this.TestToken.Arguments.Count.ToString()));
        }

        [TestMethod]
        public void Test_DecodeToken()
        {
            this.Test_CreateToken();
            var decodedToken = Token.DecodeToken(this.TestToken.AuthenticationToken, this.EncryptionPass);

            Assert.IsNotNull(decodedToken);
            Assert.IsInstanceOfType(decodedToken, typeof(Token));
            System.Console.WriteLine(string.Format("Token decoded: {0}\nNumber of Args: {1}\nInput Token: {2}", decodedToken.AuthenticationToken, decodedToken.Arguments.Count.ToString(), this.TestToken.AuthenticationToken));
        }

        [TestMethod]
        public void Test_IEnumerableExtensions()
        {
            var StringList = new List<string>() { "Val1", "Val2", "Random", "test" };
            var Result = StringList.FindAllIndexOf<string>("Val1");
            Assert.AreNotEqual(0, Result.Length);
            Result = StringList.FindAllIndexOf<string>("Val11");
            Assert.AreEqual(0, Result.Length);
        }
    }
}