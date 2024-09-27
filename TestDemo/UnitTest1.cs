using Xunit.Sdk;
using Day6Mydemo.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace TestDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DemosController demo = new DemosController();
            ViewResult result = demo.TestAction() as ViewResult;
            Assert.AreEqual("smart software", result.ViewData["name"]);
        }

        public int div(int x , int y)
        {
            return x / y;
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(50, div(100,2));
        }
    }
}