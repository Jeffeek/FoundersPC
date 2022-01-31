const priceCardsNodes = document.querySelectorAll(".price-card")
const buyBtnsNodes = document.querySelectorAll(".price-card__btn");
const buyBtns = Array.apply(null, buyBtnsNodes);
const priceCards = Array.apply(null, priceCardsNodes)
let activePriceCard = priceCards.find(card => card.classList.contains("active"))

if (buyBtns) {
    buyBtns.forEach(buyBtn => {
        buyBtn.addEventListener("click", evt => {
            evt.preventDefault();
            if (isAuthorize()) {
                buyCard(buyBtn);
            }
            else {
                openAuthModal();
            }
        });
    })
}


priceCards.forEach(card => {
    if (window.innerWidth > 820) {
        card.addEventListener("click", (evt) => {
            activePriceCard.classList.remove("active")
            evt.currentTarget.classList.add("active") 
            slide(evt.currentTarget)
            activePriceCard = evt.currentTarget;
        })
    }
})

function slide(selectedCard) {
    const selectedCardIndex = priceCards.findIndex(card => card == selectedCard);
    const activeCardIndex = priceCards.findIndex(card => card == activePriceCard);

    let needToTranslate

    if (selectedCardIndex < activeCardIndex) {
        needToTranslate = activePriceCard.getBoundingClientRect().left - selectedCard.getBoundingClientRect().left
        translateCards(needToTranslate)
    }
    else if (selectedCardIndex > activeCardIndex) {
        needToTranslate = selectedCard.getBoundingClientRect().left - activePriceCard.getBoundingClientRect().left
        translateCards(-needToTranslate)
    }

}

function translateCards(pixels) {
    let style = window.getComputedStyle(activePriceCard)
    let matrix = new WebKitCSSMatrix(style.transform)
    let currentTranslateX = matrix.m41

    priceCards.forEach(card => {
        card.style.transform = `translateX(${currentTranslateX + pixels}px)`;
    })
}

function buyCard(btn) {
    const plan = btn.getAttribute("data-plan");
    const planData = {
        "packageType": plan
    };

    fetch(window.configuration.baseUrl + "api/buy", {
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + getToken()
        },
        method: "POST",
        body: JSON.stringify(planData)
    }).then(resp => console.log(resp))
    .catch(err => console.log(err));
}