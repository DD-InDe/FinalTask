if (Notification.permission !== "granted"){
    Notification.requestPermission()
}

window.showNotification = function (title,body) {
    if (Notification.permission === "granted"){
        new Notification(title,{body:body})
    }
}