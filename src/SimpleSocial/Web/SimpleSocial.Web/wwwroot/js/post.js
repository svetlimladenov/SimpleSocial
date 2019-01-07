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




const posts = document.querySelectorAll('.single-post');
const modal = document.querySelector('.modal');
const overlay = document.querySelector('.overlay');

function toggleModal() {
    modal.classList.toggle('modal-show');
    overlay.classList.toggle('overlay-show');
}

posts.forEach(post => {
    post.querySelector('button.show-likes').addEventListener('click', () => {
        const likes = post.querySelector('.likes');
        modal.querySelector('.body').innerHTML = likes.innerHTML;
        toggleModal();
    });
});

modal.querySelector('button.close-likes').addEventListener('click', toggleModal);

posts.forEach(post => {
    post.querySelector('.like-btn').addEventListener('click', () => {
        var likeBtn = post.querySelector('button.like-btn');
        var dataLike = likeBtn.dataset.like.toLowerCase();
        var postId = likeBtn.dataset.postid;

        jQuery.ajax({
            type: "POST",
            url: "/Likes/GetAction",
            data: { isLiked: dataLike, postId: postId},
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
                } else {
                    likeBtn.innerHTML = "";
                    likeBtn.innerHTML = "<i class=\"far fa-thumbs-down\"></i>Unlike";
                }
            }
        });
    });
});