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
            document.querySelector('#areaForComment').value = '';

            createComment(data);
        }
    });
}

function createComment(data) {
    var clonedNode = document.querySelector('.comment-list').cloneNode(true);

    var date = moment.utc(data.createdOn).local().format("llll");

    clonedNode.setAttribute('id', 'replyItem' + data.id);
    clonedNode.querySelector('.thumb img').src = data.userImageUrl;
    clonedNode.querySelector('.comment').innerHTML = data.description;
    clonedNode.querySelector('#userName').innerHTML = data.userUserName;
    clonedNode.querySelector('.date').innerHTML = date;
    clonedNode.querySelector('.date').dateTime = data.createdOn;
    clonedNode.querySelector('.date').title = data.createdOn;
    clonedNode.querySelector('.button').setAttribute("onClick", "showReply(" + data.id + ")");
    clonedNode.querySelector('.hide').setAttribute('id', data.id);
    clonedNode.querySelector('.form-control').setAttribute('id', 'areaForReply' + data.id);
    clonedNode.querySelector('#buttonForReply').setAttribute("onClick", "addReply(" + data.id + "," + "'" + data.postId + "'" + ")");
    clonedNode.querySelector('.userReply').remove();

    $('#commentItem').append(clonedNode);
}

// Show reply area function
function showReply(id) {
    var reply = document.getElementById(id);
    if (reply.className === "show") {
        reply.className = "hide";
    }
    else {
        reply.className = "show";
    }
}

//  Add reply function
function addReply(commentId, postId) {
    var json = { postId: postId, commentId: commentId, description: $("#areaForReply" + commentId).val() };
    var token = $("#commentForm input[name=__RequestVerificationToken]").val();
    $.ajax({
        url: "/api/reply",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { "X-CSRF-TOKEN": token },
        success: function (data) {
            document.querySelector('#areaForReply' + commentId).value = '';

            createReply(data, commentId);
        }
    });
}

function createReply(data, commentId) {
    var clonedNode = document.querySelector('.userReply').cloneNode(true);

    var date = moment.utc(data.createdOn).local().format("llll");

    clonedNode.querySelector('.thumb img').src = data.userImageUrl;
    clonedNode.querySelector('.comment').innerHTML = data.description;
    clonedNode.querySelector('#ReplyUserName').innerHTML = data.userUserName;
    clonedNode.querySelector('.date').innerHTML = date;
    clonedNode.querySelector('.date').dateTime = data.createdOn;
    clonedNode.querySelector('.date').title = data.createdOn;

    console.log(clonedNode);

    $('#replyItem' + commentId).append(clonedNode);
}

// Show comments
function showComments() {
    $([document.documentElement, document.body]).animate({
        scrollTop: $("#commentItem").offset().top
    }, 1000);
    return false;
}

//Datetime format to current
$(function () {
    $("time").each(function (i, e) {
        var dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        var time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("llll"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});