using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Stable_Lib.Models
{
    public struct Announcement : IFirestoreSerializable
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime LastModified { get; set; }
        public string Title { get; set; }

        public Dictionary<string, object> ToFirestoreObject()
        {
            return new Dictionary<string, object>() {
                {"author", Author},
                {"body", Body},
                {"lastModified", LastModified.ToUniversalTime()},
                {"title", Title}
            };
        }

        public void GetObject(Dictionary<string, object> firestoreObject)
        {
            Author = firestoreObject["author"] as string;
            Body = firestoreObject["body"] as string;
            LastModified = ((Timestamp) firestoreObject["lastModified"]).ToDateTime();
            Title = firestoreObject["title"] as string;
        }
    }
}