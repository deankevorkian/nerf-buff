var apiKey = "646f6e96-625e-4ef9-a1c8-6a239865e4af";
var apiKey69 = "4f3cbc19371a8e60cd38ec2b253720d9";

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
        type: "POST",
        dataType: "json",
        url: "https://fortnite-public-api.theapinetwork.com/prod09/br_motd/get", // "https://api.fortnitetracker.com/v1/challenges",
        headers: {
            'Authorization': apiKey69,
            'X-Fortnite-API-Version': 'v1',
            'content-type': 'multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW'
        }
    }).then(callback => {
        console.log(callback);
        var html = '<div>';
        for (var entry of callback.entries) {
            html += '<h1>' + entry.title + '</h1>';
            // html += '<h2>Time: ' + new Date(+entry.time).toDateString() + '</h2>';
            html += '<img src="' + entry.image + '" />';
            html += '<div>' + entry.body + '</div>';
        }
        html += '</div>';
        $("#events").html(html);
    });
}

function GetRecommendedEvent() {
    $.ajax({
        type: "GET",
        url: "Events/GetRecommendedEventForUser"
    }).then(item => {
        var html = '<tr>'
            + '<td>'
            + item.Title
            + '</td>'
            + '<td>'
            + item.Time
            + '</td>'
            + '<td>'
            + item.Long
            + '</td>'
            + '<td>'
            + item.Lat
            + '</td>'
            + '<td>'
            + item.Author
            + '</td>'
            + '<td>'
            + '</td>'
            + '</tr> ';
        $("#recommendedEvent").html(html);
    });
}