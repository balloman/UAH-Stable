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

        public override Dictionary<string, object> ToFirestoreObject()
        {
            return new Dictionary<string, object>() {
                {"author", Author},
                {"body", Body},
                {"lastModified", LastModified.ToUniversalTime()},
                {"title", Title}
            };
        }

        public override void FromDict(Dictionary<string, object> firestoreObject)
        {
            Author = firestoreObject["author"] as string;
            Body = firestoreObject["body"] as string;
            LastModified = ((Timestamp) firestoreObject["lastModified"]).ToDateTime();
            Title = firestoreObject["title"] as string;
        }

        public Announcement(Dictionary<string, object> firestoreObject)
        {
            FromDict(firestoreObject);
        }
        
        public Announcement(){}
        
    }
}