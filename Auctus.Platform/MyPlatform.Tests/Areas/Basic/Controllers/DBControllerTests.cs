using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPlatform.Areas.Basic.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Areas.Basic.Controllers.Tests
{
    [TestClass()]
    public class DBControllerTests
    {
        [TestMethod()]
        public void ListTest()
        {
            DBController dc = new DBController();
            HttpResponseMessage result = dc.List();
            Assert.IsNotNull(result);
        }
    }
}