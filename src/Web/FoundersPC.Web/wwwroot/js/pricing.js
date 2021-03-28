// Slider(all Slides in a container)
const slider = document.querySelector(".pricing-slider");
// All trails 
const trail = document.querySelector(".price-trail").querySelectorAll("div");
// Transform value
let value = 0;
// price-trail index number
let trailValue = 0;
// interval (Duration)
let interval = 5000;
// Function to slide forward
const slide = (condition) =>
{
    // CLear interval
    clearInterval(start);
    // update value and trailValue
    condition === "increase" ? initiateINC() : initiateDEC();
    // move slide
    move(value, trailValue);
    // Restart Animation
    animate();
    // start interal for slides back 
    start = setInterval(() => slide("increase"), interval);
};
// function for increase(forward, next-price-card) configuration
const initiateINC = () =>
{
    // Remove price-trail-active from all trails
    trail.forEach(cur => cur.classList.remove("price-trail-active"));
    // increase transform value
    value === 80 ? value = 0 : value += 20;
    // update trailValue based on value
    trailUpdate();
};
// function for decrease(backward, previous) configuration
const initiateDEC = () =>
{
    // Remove price-trail-active from all trails
    trail.forEach(cur => cur.classList.remove("price-trail-active"));
    // decrease transform value
    value === 0 ? value = 80 : value -= 20;
    // update trailValue based on value
    trailUpdate();
};
// function to transform slide 
const move = (S, T) =>
{
    // transform pricing-slider
    slider.style.transform = `translateX(-${S}%)`;
    //add price-trail-active class to the current price-trail
    trail[T].classList.add("price-trail-active");
};
const tl = gsap.timeline({ defaults : { duration : 0.6, ease : "power2.inOut" } });
tl.from(".bg", { x : "-100%", opacity : 0 }).from("p", { opacity : 0 }, "-=0.3").from("h1", { opacity : 0, y : "30px" }, "-=0.3").from(".container-pricing-slider a", { opacity : 0, y : "-40px" }, "-=0.8");
// function to restart animation
const animate = () => tl.restart();
// function to update trailValue based on slide value
const trailUpdate = () =>
{
    if (value === 0)
    {
        trailValue = 0;
    }
    else if (value === 20)
    {
        trailValue = 1;
    }
    else if (value === 40)
    {
        trailValue = 2;
    }
    else if (value === 60)
    {
        trailValue = 3;
    }
    else
    {
        trailValue = 4;
    }
};
// Start interval for slides
let start = setInterval(() => slide("increase"), interval);
// Next  and  Previous button function (SVG icon with different classes)
document.querySelectorAll(".container-pricing-slider svg").forEach(cur =>
{
    // Assign function based on the class Name("next-price-card" and "prev-price-card")
    cur.addEventListener("click", () => cur.classList.contains("next-price-card") ? slide("increase") : slide("decrease"));
});
// function to slide when price-trail is clicked
const clickCheck = (e) =>
{
    // CLear interval
    clearInterval(start);
    // remove price-trail-active class from all trails
    trail.forEach(cur => cur.classList.remove("price-trail-active"));
    // Get selected price-trail
    const check = e.target;
    // add price-trail-active class
    check.classList.add("price-trail-active");
    // Update slide value based on the selected price-trail
    if (check.classList.contains("price-box-low"))
    {
        value = 0;
    }
    else if (check.classList.contains("price-box-medium"))
    {
        value = 20;
    }
    else if (check.classList.contains("price-box-high"))
    {
        value = 40;
    }
    else if (check.classList.contains("price-box-ultra"))
    {
        value = 60;
    }
    else
    {
        value = 80;
    }
    // update price-trail based on value
    trailUpdate();
    // transfrom slide
    move(value, trailValue);
    // start animation
    animate();
    // start interval
    start = setInterval(() => slide("increase"), interval);
};
// Add function to all trails
trail.forEach(cur => cur.addEventListener("click", (ev) => clickCheck(ev)));
// Mobile touch Slide Section
const touchSlide = (() =>
{
    let start, move, change, sliderWidth;
    // Do this on initial touch on screen
    slider.addEventListener("touchstart", (e) =>
    {
        // get the touche position of X on the screen
        start = e.touches[0].clientX;
        // (each slide with) the width of the pricing-slider container divided by the number of slides
        sliderWidth = slider.clientWidth / trail.length;
    });
    // Do this on touchDrag on screen
    slider.addEventListener("touchmove", (e) =>
    {
        // prevent default function
        e.preventDefault();
        // get the touche position of X on the screen when dragging stops
        move = e.touches[0].clientX;
        // Subtract initial position from end position and save to change variabla
        change = start - move;
    });
    const mobile = (e) =>
    {
        // if change is greater than a quarter of sliderWidth, next-price-card else Do NOTHING
        change > (sliderWidth / 4) ? slide("increase") : null;
        // if change * -1 is greater than a quarter of sliderWidth, prev-price-card else Do NOTHING
        (change * -1) > (sliderWidth / 4) ? slide("decrease") : null;
        // reset all variable to 0
        [start, move, change, sliderWidth] = [0, 0, 0, 0];
    };
    // call mobile on touch end
    slider.addEventListener("touchend", mobile);
})();