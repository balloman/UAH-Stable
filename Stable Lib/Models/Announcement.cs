using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Stable_Lib.Models
{
    public sealed class Announcement : FirestoreObject
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime LastModified { get; set; }
        public string Title { get; set; }
        public List<string> College = new List<string>();

        public override Dictionary<string, object> ToFirestoreObject()
        {
            return new Dictionary<string, object> {
                {"author", Author},
                {"body", Body},
                {"lastModified", LastModified.ToUniversalTime()},
                {"title", Title},
                {"college", College}
            };
        }

        public override void FromDict(Dictionary<string, object> firestoreObject)
        {
            Author = firestoreObject["author"] as string;
            Body = firestoreObject["body"] as string;
            LastModified = ((Timestamp) firestoreObject["lastModified"]).ToDateTime();
            Title = firestoreObject["title"] as string;
            try {
                foreach (var college in (List<object>) firestoreObject["college"]) {
                    College.Add(college as string);
                }
            }
            catch (Exception e) {
                if (e is KeyNotFoundException || e is NullReferenceException) {
                    Console.WriteLine("College not found...");
                }
            }
        }

        public Announcement(Dictionary<string, object> firestoreObject)
        {
            FromDict(firestoreObject);
        }
        
        public Announcement(){}
        
    }
}