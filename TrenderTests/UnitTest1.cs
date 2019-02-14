using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrenderFacade;

namespace TrenderTests
{
    [TestClass]
    public class DatabaseFacadeTests
    {
        [TestMethod]
        public void TestGetScheduledTasks()
        {
            DatabaseFacade facade = new DatabaseFacade("");
            var result = facade.GetScheduledTasksToRun();

            Assert.IsNotNull(result);
        }
    }
}
