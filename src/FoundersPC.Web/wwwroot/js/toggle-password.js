const togglePasswordBtn = document.querySelector("#togglePassword")
const toggleSignUpPassword = document.querySelector("#toggleSignUpPassword")
const passwordInput = document.querySelector("#password-input")
const password1 = document.querySelector("#password-1");

if (togglePasswordBtn) {
    togglePasswordBtn.addEventListener("click", e => {
        const inputType = passwordInput.getAttribute("type") === "password" ? "text" : "password"
        passwordInput.setAttribute("type", inputType)
        
        if (togglePasswordBtn.classList.value === "bi bi-eye")
            togglePasswordBtn.classList.value = "bi bi-eye-slash"
        else
            togglePasswordBtn.classList.value = "bi bi-eye"
    })
}

if (toggleSignUpPassword) {
    toggleSignUpPassword.addEventListener("click", e => {
        const inputType = password1.getAttribute("type") === "password" ? "text" : "password"
        password1.setAttribute("type", inputType)
        
        if (toggleSignUpPassword.classList.value === "bi bi-eye")
            toggleSignUpPassword.classList.value = "bi bi-eye-slash"
        else
            toggleSignUpPassword.classList.value = "bi bi-eye"
    })
}