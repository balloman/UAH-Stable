using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace Stable_Lib.Models
{
    public struct Petition : IFirestoreSerializable
    {

        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public int Votes { get; set; }
        
        public Dictionary<string, object> ToFirestoreObject()
        {
            return new Dictionary<string, object>() {
                {"author", Author},
                {"body", Body},
                {"created", Created.ToUniversalTime()},
                {"title", Title},
                {"votes", Votes}
            };
        }

        public void GetObject(Dictionary<string, object> firestoreObject)
        {
            Author = firestoreObject["author"] as string;
            Body = firestoreObject["body"] as string;
            Created = ((Timestamp) firestoreObject["created"]).ToDateTime();
            Title = firestoreObject["title"] as string;
            Votes = Convert.ToInt32((long) firestoreObject["votes"]);
        }
    }
}