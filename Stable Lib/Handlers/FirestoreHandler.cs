using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Stable_Lib.Models;
using WriteResult = Google.Cloud.Firestore.WriteResult;

namespace Stable_Lib.Handlers
{
    public class FirestoreHandler
    {
        // ReSharper disable once InconsistentNaming
        private static FirestoreHandler Instance;
        private FirestoreDb db;
        private DocumentReference postsRef;
        
        public FirestoreHandler()
        {
            Console.WriteLine("Grabbing reference to database...");
            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            db = FirestoreDb.Create("uah-stable", new FirestoreClientBuilder() {
                JsonCredentials = new StreamReader(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Stable_Lib.stable_key.json") ?? 
                                                   throw new FileNotFoundException()).ReadToEnd()
            }.Build());
            Console.WriteLine("Reference to database received");
            postsRef = db.Collection("userContent").Document("posts");
        }

        /// <summary>
        /// Retrieves posts of a given collection
        /// </summary>
        /// <param name="collection">The collection to grab the posts from</param>
        /// <returns>The posts as document snapshots</returns>
        public async Task<QuerySnapshot> GetPostsAsync(string collection)
        {
            Console.WriteLine($"Retrieving posts of collection {collection}");
            var snapshot =  await postsRef.Collection(collection).GetSnapshotAsync();
            Console.WriteLine("Posts retrieved!");
            return snapshot;
        }

        /// <summary>
        ///  Asynchronously attempts to retrieve a singular post from the given collection
        /// </summary>
        /// <param name="collection">The collection to retrieve the post form</param>
        /// <param name="documentId">the id of the post to retrieve</param>
        /// <returns>A document snapshot of the post</returns>
        public async Task<DocumentSnapshot> GetPost(string collection, string documentId)
        {
            Console.WriteLine($"Retrieving post from collection {collection} of id {documentId}");
            var docRef = await postsRef.Collection(collection).Document(documentId).GetSnapshotAsync();
            Console.WriteLine("Post retrieved!");
            return docRef;
        }

        /// <summary>
        /// Asynchronously uploads a new post to the server
        /// </summary>
        /// <param name="collection">The type of post to upload</param>
        /// <param name="post">the post</param>
        /// <returns></returns>
        public async Task<DocumentReference> UploadNewPost(string collection, FirestoreObject post)
        {
            Console.WriteLine($"Uploading new posts to collection {collection}");
            var docRef = await postsRef.Collection(collection).AddAsync(post.ToFirestoreObject());
            Console.WriteLine("Post uploaded!");
            return docRef;
        }

        public async Task<WriteResult> CreateUser(string uid, Dictionary<string, object> data)
        {
            Console.WriteLine("Adding userinfo to database");
            var docRef = await db.Collection("users").Document(uid).CreateAsync(data);
            return docRef;
        }

        public async Task<DocumentSnapshot> GetUser(string uid)
        {
            Console.WriteLine("Retrieving user");
            var docRef = await db.Collection("users").Document(uid).GetSnapshotAsync();
            return docRef;
        }

        public static FirestoreHandler GetInstance()
        {
            //Null coalescing, essentially checks if variable is null, and if it is creates a new
            return Instance ??= new FirestoreHandler();
        }
    }
}