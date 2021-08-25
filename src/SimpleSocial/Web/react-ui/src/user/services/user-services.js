function loadUserData(userId) {
  return fetch(`https://localhost:5003/Account/GetUserBoxInfo/${userId}`).then(
    (res) => res.json()
  );
}

export { loadUserData };
