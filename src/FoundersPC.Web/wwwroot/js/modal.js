const authButton = document.querySelector("#sign-in-btn");
const authModal = document.querySelector(".login-modal");
const modalBackdrop = document.querySelector("#modal-backdrop");
const createNewAccountBtn = document.querySelector(".create-new-account-btn");
const signUpModal = document.querySelector(".signup-modal");

createNewAccountBtn.addEventListener("click", e => {
    e.preventDefault();
    openSignUpModal();
})

authButton.addEventListener("click", e => {
    e.preventDefault();
    openAuthModal();
})

modalBackdrop.addEventListener("click", e => {
    closeModal();
})

function openSignUpModal() {
    signUpModal.style.display = "flex";
    document.body.classList.add("modal-open");
    modalBackdrop.classList.add("modal-backdrop");
}

function openAuthModal() {
    document.body.classList.add("modal-open");
    modalBackdrop.classList.add("modal-backdrop");
    authModal.style.display = "flex";
}

function closeModal() {
    authModal.style.display = "none";
    signUpModal.style.display = "none";
    document.body.classList.remove("modal-open");
    modalBackdrop.classList.remove("modal-backdrop");
}