using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demo.GameOfLife.UserManagement.BLL.Helpers;
using System.Diagnostics;

namespace Demo.GameOfLife.UserManagement.Tests
{
    [TestClass]
    public class HashHelperTests
    {
        private IHashHelper _sut;
        [TestInitialize]
        public void Setup()
        {
            _sut = new HashHelper();
        }
        [TestMethod]
        public void GetHash_WhenGivenSameString_ReturnTheSameHash()
        {
            var someString = "password123";//Guid.NewGuid().ToString();

            var returnValue = _sut.GetHashString(someString);
            Debug.WriteLine("The value is {0} and the hash {1}.", someString, returnValue);
            for(var i = 0; i< 10; i++)
            {
                Assert.AreEqual(returnValue, _sut.GetHashString(someString));
            }
        }
    }
}
