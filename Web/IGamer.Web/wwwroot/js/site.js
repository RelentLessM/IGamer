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
    var clonedNode = document.createElement('div');
    if (document.querySelector('.comment-list') == null)
    {
        clonedNode.innerHTML = createNode();
    }
    else
    {
        clonedNode = document.querySelector('.comment-list').cloneNode(true);
    }

    var date = moment.utc(data.createdOn).local().format("llll");


    clonedNode.querySelector('.thumb img').src = data.userImageUrl;
    clonedNode.querySelector('.comment').innerHTML = data.description;
    clonedNode.querySelector('#userName').innerHTML = data.userUserName;
    clonedNode.querySelector('.date').innerHTML = date;
    clonedNode.querySelector('.date').dateTime = data.createdOn;
    clonedNode.querySelector('.date').title = data.createdOn;
    clonedNode.querySelector('.button').setAttribute("onClick", `showReply(${data.id})`);
    clonedNode.querySelector('.replyForm').setAttribute('id', data.id);
    clonedNode.querySelector('.form-control').setAttribute('id', `areaForReply${data.id}`);
    clonedNode.querySelector('#buttonForReply').setAttribute("onClick", `addReply(${data.id},'${data.postId}')`);
    clonedNode.querySelector('.userRepliesList').innerHTML = '';
    clonedNode.querySelector('.userRepliesList').setAttribute('id', `replyItem${data.id}`);


    $('#commentItem').append(clonedNode);
}

//if first comment
function createNode() {
    return `<div class="comment-list border border-dark rounded" id="replyItem@(comment.Id)">
<div class="single-comment justify-content-between d-flex">
<div class="user justify-content-between d-flex">
<div class="thumb"><img src="@comment.UserImageUrl" alt="author" id="authorImage"></div>
<div class="desc"><p class="comment" id="commentContent">@comment.Description</p><div class="d-flex justify-content-between">
<div class="d-flex align-items-center"><h5><a asp-area="" asp-action="ByUser" asp-controller="Posts" asp-route-username="@comment.UserUserName" id="userName">
@comment.UserUserName</a></h5><time datetime="@comment.CreatedOn.ToString("O")" class="date" id="date">
</time></div></div></div></div><div class="reply-btn" align="right">
<input type="button" onclick="showReply('@comment.Id')" class="button button-contactForm btn_1" value="reply">
</div></div><div class="hide replyForm" id="@comment.Id">
<textarea class="form-control form-control-bg-tr w-100" cols="60" rows="6" placeholder="Write Reply" id="areaForReply@(Model.CommentId)">
</textarea><a role="button" onclick="addReply(@Model.CommentId, '@Model.PostId')" class="button button-contactForm btn_1" 
id="buttonForReply">Send Message <i class="flaticon-right-arrow"></i></a></div><div class="userRepliesList" id="replyItem@(comment.Id)"></div>`;
}

// Show reply area function
function showReply(id) {
    var reply = document.getElementById(id);
    if (reply.classList.contains("show")) {
        reply.classList.replace("show", "hide");
    }
    else {
        reply.classList.replace("hide", "show");
    }
}

//  Add reply function
function addReply(commentId, postId) {
    var json = { postId: postId, commentId: commentId, description: $(`#areaForReply${commentId}`).val() };
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
    var clonedNode = document.createElement('div');
    if (document.querySelector('.userReply') == null) {
        clonedNode.innerHTML = createReplyNode();
    } else {
        clonedNode = document.querySelector('.userReply').cloneNode(true);
    }

    var date = moment.utc(data.createdOn).local().format("llll");

    clonedNode.querySelector('.thumb img').src = data.userImageUrl;
    clonedNode.querySelector('.comment').innerHTML = data.description;
    clonedNode.querySelector('#ReplyUserName').innerHTML = data.userUserName;
    clonedNode.querySelector('.date').innerHTML = date;
    clonedNode.querySelector('.date').dateTime = data.createdOn;
    clonedNode.querySelector('.date').title = data.createdOn;

    $('#replyItem' + commentId).append(clonedNode);
}

// Create node if first reply
function createReplyNode() {
    return `<div class="single-comment userReply justify-content-between d-flex border border-dark rounded">
<div class="user justify-content-between d-flex"><div class="thumb"><img src="@reply.UserImageUrl" alt="author">
</div><div class="desc"><p class="comment">@reply.Description</p><div class="d-flex justify-content-between">
<div class="d-flex align-items-center"><h5>
<a asp-area="" asp-action="ByUser" asp-controller="Posts" asp-route-username="@reply.UserUserName" id="ReplyUserName">
@reply.UserUserName</a></h5><time datetime="@reply.CreatedOn.ToString("O")" class="date"></time></div></div></div></div></div>`;
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