using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConDep.implementation.Managers;

namespace ConDep.implementation.test.Managers
{
    [TestClass]
    public class WorkflowManagerTest
    {
        [TestMethod]
        public void TestReadAcitivtyFromDll()
        {
            var manager = new WorkflowManager();

            var activity = manager.ReadActivityFromDll("");
        }
    }
}
