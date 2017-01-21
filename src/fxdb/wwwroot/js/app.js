// Empty #content-container, set all navbar to inactive.
function resetState() {
    $("#navbar>ul>li.active").removeClass("active");
    $("#content-container").empty();
}

routie("",
    function () { // Home
        resetState();
        $("#navbar-button-home").addClass("active");
        $.getJSON({
            url: "/api/fx",
            cache: false,
            success: function (data) {
                console.log(data);
                console.log("rendering template");
                var template = $("#template-effect-list").html();
                Mustache.parse(template);
                var rendered = Mustache.render(template, data);
                $("#content-container").html(rendered);
            }
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