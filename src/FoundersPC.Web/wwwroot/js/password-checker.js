const passwordLevel = document.querySelector(".password-level");
const passwordMessage = document.querySelector(".password-msg");
const password = document.querySelector("#password-1");
const password2 = document.querySelector("#password-2");
const emailInput = document.querySelector("#email-input");
const loginInput = document.querySelector("#login-input");
const signupButton = document.querySelector(".signup-form .signup-form__btn");
const generatePasswordBtn = document.querySelector(".generate-password-btn");

password2.addEventListener("input", checkPasswordSafety);
password.addEventListener("input", checkPasswordSafety);
emailInput.addEventListener("input", checkPasswordSafety);
loginInput.addEventListener("input", checkPasswordSafety);


generatePasswordBtn.addEventListener("click", evt => {
    const generatedPassword = randomString();
    password.value = generatedPassword;
    password2.value = generatedPassword;
    checkPasswordSafety();
});

const randomInt = (min, max) =>
{
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
};
const randomString = (length = 11) =>
{
    let result = "";
    while (result.length < length)
    {
        result += Math.random().toString(36).slice(2);
    }
    return result;
};

function checkPasswordSafety() {
    let inputValue = password.value;

    if (inputValue.length === 0) {
        passwordLevel.classList.remove("show");
        passwordMessage.classList.remove("show");
        passwordLevel.classList.remove("medium-password");
        passwordMessage.classList.remove("medium-password");
        passwordLevel.classList.remove("good-password");
        passwordMessage.classList.remove("good-password");
        signupButton.disabled = true;
    }
    else if (inputValue.length < 7) {
        if (!passwordLevel.classList.contains("show")) {
            passwordLevel.classList.add("show");
            passwordMessage.classList.add("show");
        }
        passwordLevel.classList.remove("medium-password");
        passwordMessage.classList.remove("medium-password");
        passwordLevel.classList.remove("good-password");
        passwordMessage.classList.remove("good-password");
        passwordMessage.textContent = "Poor password"
        passwordLevel.classList.add("show");
        passwordMessage.classList.add("show");
    }
    else if (inputValue.length > 6 && inputValue.length < 11) {
        if (!passwordLevel.classList.contains("show")) {
            passwordLevel.classList.add("show");
            passwordMessage.classList.add("show");
        }
        passwordLevel.classList.remove("good-password");
        passwordMessage.classList.remove("good-password");
        passwordLevel.classList.add("medium-password");
        passwordMessage.classList.add("medium-password");
        passwordMessage.textContent = "Not bad but you know you can do it better";
    }
    else if (inputValue.length > 10) {
        if (!passwordLevel.classList.contains("show")) {
            passwordLevel.classList.add("show");
            passwordMessage.classList.add("show");
        }
        passwordLevel.classList.add("good-password");
        passwordMessage.classList.add("good-password");
        passwordMessage.textContent = "Good password!"
    }

    if (inputValue === password2.value && inputValue.length > 6 && loginInput.value.length > 4 && emailInput.value.length > 6) {
        signupButton.disabled = false;
    }
    else
        signupButton.disabled = true;
}