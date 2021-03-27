using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stable_Lib.Handlers;
using Stable_Lib.Models;

namespace StableLibTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void UploadAnnouncementTest()
        {
            var myAnnouncement = new Announcement {
                Author = "Bernard",
                Body = "This is a sample body of text for the tests",
                LastModified = DateTime.Now,
                Title = "Test Announcement"
            };
            var firestoreHandler = FirestoreHandler.GetInstance();
            var task = firestoreHandler.UploadNewPost("announcements", myAnnouncement);
            task.Wait();
            Assert.IsTrue(task.IsCompletedSuccessfully);
        }

        [TestMethod]
        public void GetAnnouncementTest()
        {
            var myAnnouncement = new Announcement {
                Author = "Bernard",
                Body = "This is a sample body of text for the tests",
                LastModified = DateTime.Now,
                Title = "We are gonna read this test"
            };
            var firestoreHandler = FirestoreHandler.GetInstance();
            var task = firestoreHandler.UploadNewPost("announcements", myAnnouncement);
            task.Wait();
            var newTask = firestoreHandler.GetPost("announcements", task.Result.Id);
            newTask.Wait();
            var retAnnouncement = new Announcement();
            retAnnouncement.GetObject(newTask.Result.ToDictionary());
            Assert.AreEqual(retAnnouncement.ToString(), myAnnouncement.ToString());
        }
    }
}