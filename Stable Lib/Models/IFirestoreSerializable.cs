using System;
using System.Collections.Generic;

namespace Stable_Lib.Models
{
    /// <summary>
    /// This interface should be implemented by all models that are intended to be stored in Firestore.<para></para>
    /// It ensures that they implement some sort of function that will convert itself into and back from dictionary form
    /// for the database.
    /// </summary>
    public interface IFirestoreSerializable
    {
        /// <summary>
        /// Method that converts to an object that can be stored in firestore documents
        /// </summary>
        /// <returns>A dictionary containing the data</returns>
        public Dictionary<string, object> ToFirestoreObject();

        /// <summary>
        /// Converts a firestore dictionary object to the given class
        /// </summary>
        /// <param name="firestoreObject">a dictionary object from firestore</param>
        public void GetObject(Dictionary<string, object> firestoreObject);
    }
}