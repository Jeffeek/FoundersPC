const authButton = document.querySelector("#sign-in-btn");
const authModal = document.querySelector(".login-modal");
const modalBackdrop = document.querySelector("#modal-backdrop");
const createNewAccountBtn = document.querySelector(".create-new-account-btn");
const signUpModal = document.querySelector(".signup-modal");
const profileModal = document.querySelector(".profile-modal");

// profile tokens

const profileKeysTemplate = document.querySelector("#profile-key-template");
const profileKeysList = document.querySelector(".profile-modal__keys");

function createToken() {
    const template = profileKeysTemplate.content.cloneNode(true);
    const keyItem = template.querySelector(".key-item");
    
    return keyItem;
}

function appendNewKeyItem(token) {
    const newToken = createToken();
    newToken.querySelector(".key-item__key").textContent = token.token;
    newToken.querySelector(".key-item__evaluation").textContent += new Date(token.activeFrom).toDateString();
    newToken.querySelector(".key-item__expired").textContent += new Date(token.activeTo).toDateString();
    if (!token.isActive) {
        newToken.classList.add("expired");
    }
    profileKeysList.appendChild(newToken);
}

async function fetchProfileKeys() {
    profileKeysList.querySelectorAll("*").forEach(n => n.remove());

    const response = await fetch(baseUrl + "api/UserInformation/Get", {
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + getToken()
        },
        method: "POST",
        body: JSON.stringify({})
    });

    if (response.ok) {
        const result = await response.json();
        // console.log(result);
        document.querySelector(".profile-modal__username").textContent = result.login;
        document.querySelector(".profile-modal__email").textContent = result.email;

        result.tokens.forEach(token => {
            appendNewKeyItem(token);
        });
    }
    else if (response.status === 401) {
        const userData = getUserData();
        const RefreshRequestData = {
            "GrantType": "RefreshToken",
            "RefreshToken": userData.refreshToken
        };

        try {
            const refreshResponse = await fetchData("api/Token", RefreshRequestData);
            pasteTokenInLocalStorage(refreshResponse);
            fetchProfileKeys();
        }
        catch (e) {
            showNotification(e.message, e.description);
            localStorage.removeItem("userData");
            window.location.href = "/";
            throw new Error()
        }
    }
    else {
        throw new Error(response.status)
    }
}

// profile tokens end

createNewAccountBtn.addEventListener("click", e => {
    e.preventDefault();
    openSignUpModal();
})

authButton.addEventListener("click", e => {
    e.preventDefault();
    if (isAuthorize())
        openProfileModal();
    else
        openAuthModal();
})

modalBackdrop.addEventListener("click", e => {
    closeModal();
})

async function openProfileModal() {

    try {
        await fetchProfileKeys();
        profileModal.style.display = "block";
        document.body.classList.add("modal-open");
        modalBackdrop.classList.add("modal-backdrop");
    }
    catch (e) {
        showNotification("Error", e);
    }
}

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
    profileModal.style.display = "none";
    document.body.classList.remove("modal-open");
    modalBackdrop.classList.remove("modal-backdrop");
}