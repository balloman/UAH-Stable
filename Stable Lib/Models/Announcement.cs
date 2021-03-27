using System;
using System.Collections.Generic;

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
                {"lastModified", LastModified},
                {"title", Title}
            };
        }

        public void GetObject(Dictionary<string, object> firestoreObject)
        {
            Author = firestoreObject["author"] as string;
            Body = firestoreObject["body"] as string;
            LastModified = (DateTime) firestoreObject["lastModified"];
            Title = firestoreObject["title"] as string;
        }
    }
}