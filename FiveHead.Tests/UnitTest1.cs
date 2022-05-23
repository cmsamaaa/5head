using FiveHead.Admin;
using FiveHead.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace FiveHead.Tests
{
    [TestClass]
    public class LoginTests

    {   
        // Test User Admin Login
        [TestMethod]
        public void CanBeLoginBy_UserIsAdmin_ReturnsTrue()
        {
            var login = new AccountsController();
            bool testresult = login.Admin_Authentication(username: "admin", password: "123");
            Assert.IsTrue(testresult);
        }

        // Test Restaurant Owner Login
        [TestMethod]
        public void CanBeLoginBy_UserIsOwner_ReturnsTrue()
        {
            var login = new StaffsController();
            bool testresult = login.Authenticate(username: "owner", password: "111");
            Assert.IsTrue(testresult);
        }

        // Test Restaurant Manager Login
        [TestMethod]
        public void CanBeLoginBy_UserIsManager_ReturnsTrue()
        {
            var login = new StaffsController();
            bool testresult = login.Authenticate(username: "manager", password: "222");
            Assert.IsTrue(testresult);
        }

        // Test Restaurant Staff Login
        [TestMethod]
        public void CanBeLoginBy_UserIsStaff_ReturnsTrue()
        {
            var login = new StaffsController();
            bool testresult = login.Authenticate(username: "staff", password: "333");
            Assert.IsTrue(testresult); // supposed to fail
        }

        // Test if empty user can login 
        [TestMethod]
        public void CanBeLoginBy_NoUser_ReturnsFalse()
        {
            var login = new StaffsController();
            bool testresult = login.Authenticate(username: "", password: "");
            Assert.IsFalse(testresult); // supposed to fail
        }

        // Test if wrong username/password can login 
        [TestMethod]
        public void CanBeLoginBy_WrongPass_ReturnsFalse()
        {
            var login = new StaffsController();
            bool testresult = login.Authenticate(username: "staff", password: "999"); // wrong password
            Assert.IsFalse(testresult); // supposed to fail
        }
    }
}
