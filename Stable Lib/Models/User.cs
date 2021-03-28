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

        public User(string email, string name, List<string> posts)
        {
            Email = email;
            Name = name;
            Posts = posts;
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

        public static User DefaultUser()
        {
            return new User(new Dictionary<string, object>() {
                {"email", "user@user.com"},
                {"name", "Default User"},
                {"posts", new List<object>()}
            });
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