window.connection = null;

if (window.connection === null) {
    window.connection = new signalR.HubConnectionBuilder()
        .withUrl("/notification-hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    async function start() {
        try {
            await window.connection.start();
            console.log("SignalR Connected.");

            await window.connection.invoke("JoinGroup");

        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }

    window.connection.onclose(async () => {
        await start();
    });
     
    start();
}

// Doktora bildirim gönderme
async function SendNotificationToDoctor(message, doctorId) {
    try {
        await window.connection.invoke("SendNotificationToDoctor", message, doctorId);
        console.log("Notification sent to Doctor.");
    } catch (err) {
        console.error(err);
    }
}

// Radyoloji teknisyenlerine bildirim gönderme
async function SendNotificationToRadiologyTechnicians(message) {
    try {
        await window.connection.invoke("SendNotificationToRadiologyTechnicians", message);
        console.log("Notification sent to all Radiology Technicians.");
    } catch (err) {
        console.error(err);
    }
}

