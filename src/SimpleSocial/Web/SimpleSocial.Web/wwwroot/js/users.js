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