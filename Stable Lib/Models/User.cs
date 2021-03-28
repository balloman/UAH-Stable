using System.Collections.Generic;
using System.Linq;

namespace Stable_Lib.Models
{
    public class User : FirestoreObject
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> Posts { get; set; }

        public User(Dictionary<string, object> firestoreObject)
        {
            FromDict(firestoreObject);
        }
        
        public User(){}

        public override Dictionary<string, object> ToFirestoreObject()
        {
            return new Dictionary<string, object> {
                {"email", Email},
                {"name", Name},
                {"posts", Posts}
            };
        }

        public override void FromDict(Dictionary<string, object> firestoreObject)
        {
            Email = firestoreObject["email"] as string;
            Name = firestoreObject["name"] as string;
            var l = new List<string>();
            //Really complicated method to convert an object list to a string list
            l = ((List<object>) firestoreObject["posts"]).Select(o => (string) o).ToList();
            Posts = l;
        }
    }
}