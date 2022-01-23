function pasteTokenInLocalStorage(token) {
    if (token == null || token == undefined) {
        return;
    }

    localStorage.setItem("token", token);
}

function signOut() {
    localStorage.removeItem("token");
    window.location.href = "/";
}

function signIn(token) {
    pasteTokenInLocalStorage(token);
    window.location.href = "/";
}