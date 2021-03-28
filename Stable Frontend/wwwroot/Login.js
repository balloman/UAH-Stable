(function () {
    window.FirebaseFunctions = {
        signup: function (username, pass) {
            // [START auth_signup_password]
            firebase.auth().createUserWithEmailAndPassword(username, pass)
                .then((userCredential) => {
                    // Signed in 
                    console.log(userCredential.user)
                    return userCredential.user.uid;
                })
                .catch((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log(errorCode, errorMessage)
                });
        },
        login: function (username, pass){
            firebase.auth().signInWithEmailAndPassword(email, password)
                .then((userCredential) => {
                    console.log(userCredential.user.uid)
                    return userCredential.user.uid
                })
                .catch((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log(errorCode, errorMessage)
                })
        }
    }
})();