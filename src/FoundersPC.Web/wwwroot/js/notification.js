const notificationTemplate = document.querySelector("#notification-template");
const notificationList = document.querySelector("#notification-list");

function createNotification() {
    const template = notificationTemplate.content.cloneNode(true);
    const notification = template.querySelector(".notification");
    
    return notification;
}

function showNotification(header, message, success = false) {

    if (!header && !message)
        return ;
    let notification = createNotification();

    notification.querySelector(".notification__header").textContent = header;
    notification.querySelector(".notification__message").textContent = message;

    if (success)
        notification.classList.add("success");

    notificationList.appendChild(notification);
    slideNotification(notification);

    notification.addEventListener("click", () => removeNotification(notification));
    setTimeout(removeNotification, 5000, notification);
}

function removeNotification(notification) {
    notification.style.transform = "translateX(-400px)";
    setTimeout(() => notificationList.removeChild(notification), 500);
}

function slideNotification(notification, isRemove = false) {
    let currentPos = -400;

    const slideInterval = setInterval(() => {
        if (currentPos > 0)
            clearInterval(slideInterval);
        currentPos += 200;
        notification.style.transform = `translateX(${currentPos})`;
    }, 10);
}