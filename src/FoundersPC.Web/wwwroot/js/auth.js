const signInBtn = document.querySelector("#sign-in-btn");
const signUpBtn = document.querySelector(".signup-form__btn");

if (isAuthorize()) {
    signInBtn.textContent = "Profile";
}
else {
    signInBtn.textContent = "SignIn";
}

signUpBtn.addEventListener("click", evt => {
    evt.preventDefault();
    signUp();
});

async function signUp() {
    const userData = {
        "Email": document.querySelector("#email-input").value,
        "Login": document.querySelector("#login-input").value,
        "Password": document.querySelector("#password-1").value,
        "RepeatPassword": document.querySelector("#password-2").value
    };
    let response = fetch("https://localhost:9000/api/SignUp", {
        headers: {
            "Accept": "application/json",
            "Content-Type": "",
        },
        method: "POST",
        body: JSON.stringify(userData)
    });
    
    response.then(result => result.json()).then(data => console.log(data));
}


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

function isAuthorize() {
    if (localStorage.getItem("userData"))
        return true
    return false
}