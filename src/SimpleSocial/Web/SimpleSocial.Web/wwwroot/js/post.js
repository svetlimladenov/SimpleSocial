var buttons = $(".like-btn");

$(document).ready(function () {
    $.each(buttons,
        function () {
            var likebtn = $(this);
            var isLiked = likebtn.data("like");
            if (isLiked === 'True') {
                likebtn.html("<i class=\"far fa-thumbs-down\"></i>Unlike");
            } else {
                likebtn.html("<i class=\"far fa-thumbs-up\"></i>Like");
            }
        });
});




var posts = document.querySelectorAll('.single-post');
var modal = document.querySelector('.modal');
var overlay = document.querySelector('.overlay');

function toggleModal() {
    var usersToFollow = document.querySelectorAll('.follower');
    console.log(usersToFollow);
    usersToFollow.forEach(user => {
        if (user.querySelector('button')) {
            console.log('aa');
            $(user.querySelector('button')).on('click', function (e) {
                console.log('clicked');
                console.log(e);
                var button = user.querySelector('button');
                var btnText = button.innerHTML.toLowerCase();
                var userId = button.dataset.userid;
                console.log(btnText);
                console.log(userId);

                $.ajax({
                    type: "POST",
                    url: "/Followers/ChooseAction",
                    data: { userId: userId, action: btnText.toLowerCase() },
                    success: function () {
                        console.log("successly send");
                    },
                    complete: function () {
                        if (btnText === "follow") {
                            $(button).addClass("unfollow").text("Unfollow");
                        } else if (btnText === "unfollow") {
                            $(button).removeClass("unfollow").text("Follow");
                        }
                    }
                });
            });
        } else {
            console.log('error');
        }
    });
    modal.classList.toggle('modal-show');
    overlay.classList.toggle('overlay-show');
}

posts.forEach(post => {
    $(post.querySelector('button.show-likes')).on('click', () => {
        var likes = post.querySelector('.likes');
        modal.querySelector('.body').innerHTML = likes.innerHTML;
        toggleModal();
    });
});

modal.querySelector('button.close-likes').onclick = toggleModal;

posts.forEach(post => {
    var postLikes = post.querySelector('.post-likes-number');
    var postLikesNumber = parseInt(postLikes.innerHTML);
    post.querySelector('.like-btn').onclick = function () {
        var likeBtn = post.querySelector('button.like-btn');
        var dataLike = likeBtn.dataset.like.toLowerCase();
        var postId = likeBtn.dataset.postid;
        jQuery.ajax({
            type: "POST",
            url: "/Likes/GetAction",
            data: { isLiked: dataLike, postId: postId },
            success: function () {
                if (dataLike === 'true') {
                    likeBtn.dataset.like = 'false';
                } else {
                    likeBtn.dataset.like = 'true';
                }
            },
            complete: function () {
                if (dataLike === 'true') {
                    likeBtn.innerHTML = "";
                    likeBtn.innerHTML = "<i class=\"far fa-thumbs-up\"></i>Like";
                    postLikes.innerHTML = postLikesNumber - 1;
                    postLikesNumber = parseInt(postLikes.innerHTML);
                } else {
                    likeBtn.innerHTML = "";
                    likeBtn.innerHTML = "<i class=\"far fa-thumbs-down\"></i>Unlike";
                    postLikes.innerHTML = postLikesNumber + 1;
                    postLikesNumber = parseInt(postLikes.innerHTML);
                }
            }
        });
    };
});