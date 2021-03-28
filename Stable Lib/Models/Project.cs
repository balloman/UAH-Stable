using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Stable_Lib.Models
{
    public sealed class Project : FirestoreObject
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        
        public override Dictionary<string, object> ToFirestoreObject()
        {
            return new Dictionary<string, object>() {
                {"author", Author},
                {"body", Body},
                {"created", Created.ToUniversalTime()},
                {"title", Title},
            };
        }

        public override void FromDict(Dictionary<string, object> firestoreObject)
        {
            Author = firestoreObject["author"] as string;
            Body = firestoreObject["body"] as string;
            Created = ((Timestamp) firestoreObject["created"]).ToDateTime();
            Title = firestoreObject["title"] as string;
        }
        
        public Project(){}

        public Project(Dictionary<string, object> firestoreObject)
        {
            FromDict(firestoreObject);
        }

    }
}