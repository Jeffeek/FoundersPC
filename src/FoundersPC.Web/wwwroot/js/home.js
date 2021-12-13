(function ()
{
    const e = document.documentElement;
    const hasAnimationWrapper = e.getElementsByClassName("has-animations")[0];
    if ((e.classList.remove("no-js"), e.classList.add("js"), hasAnimationWrapper != null))
    {
        (window.sr = ScrollReveal()).reveal(".pricing-table-inner",
                {
                    duration : 3000,
                    distance : "50px",
                    easing : "cubic-bezier(0.5, -0.01, 0, 1.005)",
                    origin : "top",
                    interval : 300
                }),
            e.classList.add("anime-ready"),
            anime.timeline({ targets : ".hero-figure-box-05" }).
                add(
                    {
                        duration : 400,
                        easing : "easeInOutExpo",
                        scaleX : [0.05, 0.05],
                        scaleY : [0, 1],
                        perspective : "500px",
                        delay : anime.random(300, 1200)
                    }).
                add(
                    {
                        duration : 400,
                        easing :
                            "easeInOutExpo",
                        scaleX : 1
                    }).
                add({
                    duration : 800,
                    rotateY : "-15deg",
                    rotateX : "8deg",
                    rotateZ : "-1deg"
                }),
            anime.timeline(
                    {
                        targets : ".hero-figure-box-06, .hero-figure-box-07"
                    }).
                add(
                    {
                        duration : 400,
                        easing : "easeInOutExpo",
                        scaleX : [0.05, 0.05],
                        scaleY : [0, 1],
                        perspective : "500px",
                        delay : anime.random(0, 400)
                    }).
                add(
                    {
                        duration : 400,
                        easing : "easeInOutExpo",
                        scaleX : 1
                    }).
                add(
                    {
                        duration : 800,
                        rotateZ : "20deg"
                    }),
            anime(
                {
                    targets :
                        ".hero-figure-box-01, .hero-figure-box-02, .hero-figure-box-03, .hero-figure-box-04, .hero-figure-box-08, .hero-figure-box-09, .hero-figure-box-10",
                    duration : anime.random(600, 1000),
                    delay : anime.random(700, 1200),
                    rotate : [
                        anime.random(-360, 360),
                        function (rotateObject) { return rotateObject.getAttribute("data-rotation"); }
                    ],
                    scale : [0.7, 1],
                    opacity : [0, 1],
                    easing : "easeInOutExpo"
                });
    }
})();