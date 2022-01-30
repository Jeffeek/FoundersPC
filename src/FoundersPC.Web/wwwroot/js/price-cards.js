const priceCardsNodes = document.querySelectorAll(".price-card")
const priceCards = Array.apply(null, priceCardsNodes)
let activePriceCard = priceCards.find(card => card.classList.contains("active"))

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