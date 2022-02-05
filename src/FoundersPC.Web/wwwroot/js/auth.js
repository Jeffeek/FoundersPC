const signInBtn = document.querySelector("#sign-in-btn");
const signInFormBtn = document.querySelector(".login-form__btn");
const signUpBtn = document.querySelector(".signup-form__btn");
const signOutBtn = document.querySelector("#sign-out-btn");
const forgotPasswordBtn = document.querySelector(".login-form__forgot-password-btn");
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

forgotPasswordBtn.addEventListener("click", async evt => await forgotPassword(evt));

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
        let errorText = "";
        for (key in e.errors) {
            errorText += key + ": " + e.errors[key][0] + '\n';
        }
        showNotification(e.title, errorText);
    }
}

async function signIn(evt) {
    evt.preventDefault();

    const userFormData = {
        "Login": document.querySelector("#email-or-login-input").value,
        "Password": document.querySelector("#password-input").value,
        "GrantType": "Password",
    };

    if (!userFormData.Login || !userFormData.Password)
        return ;

    try {
        const userData = await fetchData("api/token", userFormData);
        authorize(userData);
    }
    catch (e) {
        showNotification(e.message, e.description);
    }
}

async function forgotPassword(evt) {
    evt.preventDefault();

    if (isAuthorize())
        return;

    const email = document.querySelector("#email-or-login-input").value;

    if (!email || email.length === 0 || `(?:[a-z0-9!#$%&'*+/=?^_\`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_\`{|}~-]+)*|"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])`.match(email)) {
        showNotification("Forgot password error", "Entered email is incorrect, write your email email", false);
        return;
    }

    try
    {
        let response = await fetch(baseUrl + "api/ForgotPassword", {
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json; charset=utf-8"
            },
            method: "POST",
            body: JSON.stringify({
                email: email
            })
        });

        if (response.ok) {
            response = await response.json();
        }
        else {
            showNotification("Forgot password notification", "Server error", false);
            return;
        }

        if (!response.isSuccess)
            showNotification("Forgot password notification", response.message, false);
        else {
            showNotification("Forgot password notification", response.message, true);
            showNotification("Forgot password notification", "Check your email box for new password");
        }
    }
    catch(e) {
        showNotification(e.message, e.description);
    }
}

async function getProfileTokens(evt) {
    evt.preventDefault();

    if (!isAuthorize()) 
        return ;

    try {
        const profileTokens = await fetchData("api/UserInformation/Get", {});
        console.log(profileTokens);
    }
    catch(e) {
        showNotification(e.message, e.description);
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

function getUserData() {
    const jsonData = localStorage.getItem("userData");

    return JSON.parse(jsonData);
}

function getToken() {
    const userData = getUserData();

    return userData.accessToken;
}