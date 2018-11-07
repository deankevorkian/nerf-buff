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