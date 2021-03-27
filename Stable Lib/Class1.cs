using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace Stable_Lib
{
    public class Class1
    {
        public static void Main(string[] names)
        { 

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("stable_key.json")
            });

            var db = FirestoreDb.Create("uah-stable", new FirestoreClientBuilder()
            {
                JsonCredentials = new StreamReader("stable_key.json").ReadToEnd()
            }.Build());
            Console.WriteLine("Collecting Data...");
            var collecRef = db.Collection("announcements");
            var docRef = collecRef.Document();
            var documentData = new Dictionary<string, object>
            {
                {"title", "Title of the 2nd Announcement"},
                {"body", "This is a body"},
                {"dateModified", DateTime.Now.ToUniversalTime()},
                {"author", "Bernard Allotey"}
            };
            var _ = docRef.SetAsync(documentData).Result;
        }
    }
}
