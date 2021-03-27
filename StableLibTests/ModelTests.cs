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
            retAnnouncement.FromDict(newTask.Result.ToDictionary());
            Assert.AreEqual(retAnnouncement.ToString(), myAnnouncement.ToString());
        }

        [TestMethod]
        public void UploadPetitionTest()
        {
            var myPetition = new Petition() {
                Author = "Bernard",
                Body = "This is my petition to test petitions",
                Created = DateTime.Now,
                Title = "Petition Test!",
                Votes = 1
            };
            var firestoreHandler = FirestoreHandler.GetInstance();
            var task = firestoreHandler.UploadNewPost("petitions", myPetition);
            task.Wait();
            Assert.IsTrue(task.IsCompletedSuccessfully);
        }

        [TestMethod]
        public void GetPetitionTest()
        {
            var myPetition = new Petition() {
                Author = "Bernard",
                Body = "This is my petition to test petitions",
                Created = DateTime.Now,
                Title = "Petition Test!",
                Votes = 1
            };
            var firestoreHandler = FirestoreHandler.GetInstance();
            var task = firestoreHandler.UploadNewPost("petitions", myPetition);
            task.Wait();
            Assert.IsTrue(task.IsCompletedSuccessfully);
            var newTask = firestoreHandler.GetPost("petitions", task.Result.Id);
            newTask.Wait();
            var retPetition = new Petition();
            retPetition.FromDict(newTask.Result.ToDictionary());
            Assert.AreEqual(retPetition.ToString(), myPetition.ToString());
        }

        [TestMethod]
        public void GetAnnouncementsTest()
        {
            var handler = FirestoreHandler.GetInstance();
            var task = handler.GetPostsAsync("announcements").Result;
            foreach (var post in task) {
                var announcement = new Announcement();
                announcement.FromDict(post.ToDictionary());
                Console.WriteLine(announcement.ToString());
            }
        }
    }
}