var pageNumber = 0;

$(window).scroll(function () {

    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        pageNumber++;
        var url = "/Account/GetMyPosts?pageNumber=" + pageNumber;
        $.ajax({
            type: "GET",
            url: url,
            success: function (posts) {
                $('#postsHolder').append(posts);
                $.getScript('../js/post.js');

            }
        });

        posts = document.querySelectorAll('.single-post');
    }
});