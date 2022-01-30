const signInBtn = document.querySelector("#sign-in-btn");
const signInFormBtn = document.querySelector(".login-form__btn");
const signUpBtn = document.querySelector(".signup-form__btn");
const signOutBtn = document.querySelector("#sign-out-btn");
const baseUrl = window.configuration.baseUrl;

if (isAuthorize()) {
    signInBtn.textContent = "Profile";
    signInBtn.classList.remove("btn-outline-success");
    signInBtn.classList.add("btn-outline-primary");
    signOutBtn.style.display = "block";
}
else {
    signInBtn.textContent = "SignIn";
    signOutBtn.style.display = "none";
}

signInFormBtn.addEventListener("click", async evt => await signIn(evt));

signOutBtn.addEventListener("click", signOut);

signUpBtn.addEventListener("click", async evt => await signUp(evt));

async function signUp(evt) {
    evt.preventDefault();
    const userFormData = {
        "Email": document.querySelector("#email-input").value,
        "Login": document.querySelector("#login-input").value,
        "Password": document.querySelector("#password-1").value,
        "RepeatPassword": document.querySelector("#password-2").value
    };

    try {
        const userData = await fetchData("api/SignUp", userFormData);
        authorize(userData);
    }
    catch (e) {
        console.error(e);
    }
}

async function signIn(evt) {
    evt.preventDefault();
    const userFormData = {
        "Login": document.querySelector("#email-or-login-input"),
        "Password": document.querySelector("#password-input"),
        "GrantType": "Password"
    };

    try {
        const userData = await fetchData("api/token", userFormData);
        authorize(userData);
    }
    catch (e) {
        console.error(e);
    }
}

function signOut(evt) {
    evt.preventDefault();
    localStorage.removeItem("userData");
    window.location.href = "/";
}

function authorize(userData) {
    pasteTokenInLocalStorage(userData);
    window.location.href = "/";
}

function pasteTokenInLocalStorage(userData) {
    if (userData == null || userData == undefined) {
        throw new Error("userData is undefined");
    }

    localStorage.setItem("userData", JSON.stringify(userData));
}

function isAuthorize() {
    if (localStorage.getItem("userData"))
        return true
    return false
}

async function fetchData(url, data) {
    let formBody = [];

    for (let name in data) {
        var encodedKey = encodeURIComponent(name);
        var encodedValue = encodeURIComponent(data[name]);
        formBody.push(encodedKey + "=" + encodedValue);
    }
    formBody = formBody.join("&");

    const response = await fetch(baseUrl + url, {
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/x-www-form-urlencoded",
        },
        method: "POST",
        body: formBody
    });

    if (response.ok) {
        return await response.json();
    }
    else {
        throw await response.json();
    }
}