// Login callback/handling
var config = {
    authority: "https://auth.nofla.me",
    client_id: "fxdb.client",
    redirect_uri: "https://fxdb.nofla.me/callback.html",
    response_type: "id_token token",
    scope: "openid profile email fxdb.read fxdb.write",
    post_logout_redirect_uri: "https://fxdb.nofla.me/"
};
var mgr = new Oidc.UserManager(config);
function doUserLoginCheck() {
    var returnValue;
    mgr.getUser()
    .then(function (user) {
        if (user) {
            console.log("User logged in: " + JSON.stringify(user.profile));
            returnValue = user;
        } else {
            console.log("User not logged in");
            mgr.signinRedirect();
            returnValue = false;
        }
    });
    return returnValue;
}

function login() {
    mgr.signinRedirect();
}
function logout() {
    mgr.signoutRedirect();
}