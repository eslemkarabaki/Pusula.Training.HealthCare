using Microsoft.AspNetCore.SignalR;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pusula.Training.HealthCare.Doctors;
using Volo.Abp.AspNetCore.SignalR;

namespace Pusula.Training.HealthCare.Hub;

[HubRoute("hub/notification")]
public class NotificationHub : AbpHub
{
    public async Task JoinGroup(string groupName) => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    public async Task SendNotificationToDoctor(Guid doctorId, string message) =>
        await Clients.Group(doctorId.ToString()).SendAsync("ReceiveNotification", message);

    public async Task SendNotificationToRadiologyTechnicians(string message) =>
        await Clients.Group(HealthCareRoles.RadyologyTechnician).SendAsync("ReceiveNotification", message);

    public async Task GetMessage(string message) => Console.WriteLine("Message Received: " + message);
}