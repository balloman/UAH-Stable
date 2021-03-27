using System;
using System.Collections.Generic;
using System.Linq;

namespace Stable_Lib.Models
{
    /// <summary>
    /// This class defines all models that are intended to be stored in Firestore.<para></para>
    /// It ensures that they implement some sort of function that will convert itself into and back from dictionary form
    /// for the database.
    /// </summary>
    public abstract class FirestoreObject
    {
        /// <summary>
        /// Method that converts to an object that can be stored in firestore documents
        /// </summary>
        /// <returns>A dictionary containing the data</returns>
        public abstract Dictionary<string, object> ToFirestoreObject();

        /// <summary>
        /// Converts a firestore dictionary object to the given class
        /// </summary>
        /// <param name="firestoreObject">a dictionary object from firestore</param>
        public abstract void FromDict(Dictionary<string, object> firestoreObject);

        public override string ToString()
        {
            var ret = string.Empty;
            var dict = ToFirestoreObject();
            return dict.Keys.Aggregate(ret, (current, key) => current + $"{key}: {dict[key]}\n");
        }
    }
}