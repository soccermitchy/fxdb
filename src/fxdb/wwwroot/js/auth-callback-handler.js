mgr.signinRedirectCallback()
    .then(function () {
        console.log("oh wait it worked???");
        window.location.replace("/app.html");
    })
    .catch(function(e) {
        console.error(e);
    });