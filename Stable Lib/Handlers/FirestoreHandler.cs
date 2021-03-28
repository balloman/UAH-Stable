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
        private readonly DocumentReference postsRef;
        private readonly CollectionReference usersRef;
        public User user = User.DefaultUser();
        public bool LoggedIn = false;
        
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
            usersRef = db.Collection("users");
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

        public async Task<QuerySnapshot> GetPostsAsync(string collection, string ordered, bool descending)
        {
            Console.WriteLine($"Retrieving posts of collection {collection}");
            QuerySnapshot snapshot;
            if (descending) {
                snapshot =  await postsRef.Collection(collection).OrderByDescending(ordered).GetSnapshotAsync();
            }
            else {
                snapshot =  await postsRef.Collection(collection).OrderBy(ordered).GetSnapshotAsync();
            }
            
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

        /// <summary>
        /// This async method adds a post to a users post database
        /// </summary>
        /// <param name="uid">the author to add the post to</param>
        /// <param name="postId">document reference for the post</param>
        /// <returns></returns>
        public async Task<WriteResult> AddPostToUser(string uid, DocumentReference postId)
        {
            var userUpdateTask = await usersRef
                .Document(uid)
                .UpdateAsync("posts", FieldValue.ArrayUnion($"{postId.Parent.Id}/{postId.Id}"));
            return userUpdateTask;
        }

        /// <summary>
        /// Creates a user in the database
        /// </summary>
        /// <param name="uid">the uid of the user to add</param>
        /// <param name="data">the info for the user to add</param>
        /// <returns></returns>
        public async Task<WriteResult> CreateUser(string uid, Dictionary<string, object> data)
        {
            Console.WriteLine("Adding userinfo to database");
            var docRef = await db.Collection("users").Document(uid).SetAsync(data);
            return docRef;
        }

        public async Task<WriteResult> CreateUser(string uid, User user)
        {
            Console.WriteLine("Adding userinfo to database");
            var docRef = await db.Collection("users").Document(uid).SetAsync(user.ToFirestoreObject());
            return docRef;
        }

        public async Task<DocumentSnapshot> GetUser(string uid)
        {
            Console.WriteLine("Retrieving user");
            var docRef = await db.Collection("users").Document(uid).GetSnapshotAsync();
            return docRef;
        }

        public void Login(User user)
        {
            this.user = user;
            LoggedIn = true;
        }
        

        public static FirestoreHandler GetInstance()
        {
            //Null coalescing, essentially checks if variable is null, and if it is creates a new
            return Instance ??= new FirestoreHandler();
        }
    }
}