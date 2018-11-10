var apiKey = "646f6e96-625e-4ef9-a1c8-6a239865e4af";

function PostComment(id) {
    var comments = {
        //Id: 1,
        PostId: id,
        //Title: "not used lol",
        //Author: username, should be the user if he's logged in
        Content: $("#newComment" + id).val(),
        //Date: new Date(),
    };
    $.ajax({
        type: "POST",
        url: "Comments/CreateCommentAsync/",
        data: comments,
        success: function (data) {
            window.location.reload();
        },
    });
}

function LoginUser(userName, password) {
    var user = {
        BlogUserName: userName,
        BlogUserPassword: password
    };

    // Fuck I just fucking love JavaScript, especially vanilla ES5 JavaScript!! WOOHOO
    $.ajax({
        type: "POST",
        url: "Users/Login",
        data: user
    });
}

function FetchCurrentFortniteChallenges() {
    $.ajax({
        type: "GET",
        url: "https://api.fortnitetracker.com/v1/challenges",
        headers: {
            'TRN-Api-Key': apiKey
        }
    });
}