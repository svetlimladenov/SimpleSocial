function fetchUserPosts(userId) {
  return fetch(`https://localhost:5001/api/users/${userId}/posts`).then((res) =>
    res.json()
  );
}

function fetchFeedPosts() {
  // TODO:
}

export { fetchUserPosts, fetchFeedPosts };
