// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Dropdown function on click.
var blockDropdown = document.getElementById("blockDropdown");
var blockMenu = document.getElementById("blockMenu");
blockMenu.style.display = 'none';

var pagesDropdown = document.getElementById("pagesDropdown");
var pagesMenu = document.getElementById("pagesMenu");
pagesMenu.style.display = 'none';


blockDropdown.addEventListener('click', function () {
    if (pagesMenu.style.display === 'block') {
        pagesMenu.style.display = 'none';
    }

    if (blockMenu.style.display === 'block') {
        blockMenu.style.display = 'none';
    }
    else {
        blockMenu.style.display = 'block';
        
    }
});

pagesDropdown.addEventListener('click', function () {
    if (blockMenu.style.display === 'block') {
        blockMenu.style.display = 'none';
    }

    if (pagesMenu.style.display === 'block') {
        pagesMenu.style.display = 'none';
    }
    else {
        pagesMenu.style.display = 'block';
    }
});

window.onclick = function (event) {
    if (!event.target.matches('.dropdown-toggle')) {
        var dropdowns = document.getElementsByClassName("dropdown-menu");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.style.display === 'block') {
                openDropdown.style.display = 'none';
            }
        }
    }
};

// Vote function on click - like.
function like(postId) {
    var json = { postId: postId, isUpVote: true };
    var token = $("#votesForm input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: "/api/votes",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { "X-CSRF-TOKEN": token },
        success: function (data) {
            $("#votesCount").html(data.votesCount);

        }
    });
}

// Vote function on click - dislike.
function dislike(postId) {
    var json = { postId: postId, isUpVote: false };
    $.ajax({
        url: "/api/votes",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#votesCount").html(data.votesCount);

        }
    });
}

// Add comment function

function addComment(postId) {
    var json = { postId: postId, description: $("#areaForComment").val() };
    var token = $("#commentForm input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: "/api/comments",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { "X-CSRF-TOKEN": token },
        success: function (data) {


        }
    });
}