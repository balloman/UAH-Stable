(function () {
    window.FirebaseFunctions = {
        signup: async function (username, pass) {
            // [START auth_signup_password]
            var uid = "";
            await firebase.auth().createUserWithEmailAndPassword(username, pass)
                .then((userCredential) => {
                    // Signed in 
                    console.log(userCredential.user.uid)
                    uid = userCredential.user.uid;
                })
                .catch((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log(errorCode, errorMessage)
                });
            return uid;
        },
        login: async function (username, pass){
            let uid = "";
            await firebase.auth().signInWithEmailAndPassword(username, pass)
                .then((userCredential) => {
                    console.log(userCredential.user.uid)
                    uid = userCredential.user.uid;
                })
                .catch((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log(errorCode, errorMessage)
                })
            return uid;
        }
    }
})();