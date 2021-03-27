using System.Collections.Generic;

namespace Stable_Lib.Models
{
    public class User : FirestoreObject
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Posts { get; set; }

        public override Dictionary<string, object> ToFirestoreObject()
        {
            throw new System.NotImplementedException();
        }

        public override void FromDict(Dictionary<string, object> firestoreObject)
        {
            throw new System.NotImplementedException();
        }
    }
}