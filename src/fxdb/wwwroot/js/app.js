var user = doUserLoginCheck();

// Set navbar right side up
$(document)
    .ready(function() {
        var template = $("#template-navbar-userinfo").html();
        Mustache.parse(template);
        mgr.getUser()
            .then(function (user) {
                var rendered = Mustache.render(template, user.profile.name);
                $("#navbar-right-userinfo").html(rendered);
            });
    });

// Empty #content-container, set all navbar to inactive.
function resetState() {
    $("#navbar>ul>li.active").removeClass("active");
    $("#content-container").empty();
}

function setXhrAuthorizationHeader(xhr) {
    mgr.getUser()
        .then(function(user) {
            xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        });
}
// Hash routing
routie("",
    function () { // Home
        resetState();
        $("#navbar-button-home").addClass("active");
        $.ajax({
            url: "/api/fx",
            dataType: "json",
            cache: false,
            success: function (data) {
                console.log(data);
                console.log("rendering template");
                var template = $("#template-effect-list").html();
                Mustache.parse(template);
                var rendered = Mustache.render(template, data);
                $("#content-container").html(rendered);
            },
            beforeSend: setXhrAuthorizationHeader
        });
        console.log("Home route");
    });

routie("contact",
    function () { // Contact page
        resetState();
        $("#navbar-button-contact").addClass("active");
        var template = $("#template-contact-page").html();
        Mustache.parse(template);
        var rendered = Mustache.render(template, {});
        $("#content-container").html(rendered);
        console.log("Contact route");
    });

routie("upload",
    function () {
        resetState();
        $("#navbar-button-upload").addClass("active");
        var template = $("#template-upload-page").html();
        Mustache.parse(template);
        var rendered = Mustache.render(template, {});
        $("#content-container").html(rendered);
        console.log("Upload route");
    });
routie("fx/info/:id",
    function (id) {
        resetState();
        $.ajax({
            url: "/api/fx/info/" + id,
            cache: false,
            dataType: "json",
            success: function(data) {
                var template = $("#template-effect-info").html();
                Mustache.parse(template);
                var rendered = Mustache.render(template, data);
                $("#content-container").html(rendered);
            },
            beforeSend: setXhrAuthorizationHeader
        });
    });

// Form hooks
$("#upload-form")
    .submit(function(e) { // thanks, stackoverflow
        data = new FormData();
        data.append("title", $("#titleUploadFormBox").val());
        data.append("file", $("#effectUploadFileBox")[0].files[0]);

        $.ajax({
            url: "/api/fx",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            mineType: "multipart/form-data",
            success: function(data) {
                window.location.replace("#fx/info/" + data.id);
            },
            error: function(jqxhr, textStatus, errorThrown) {
                alert("Unexpected error: " + textStatus + " - " + errorThrown);
            },
            beforeSend: setXhrAuthorizationHeader
        });
    });
