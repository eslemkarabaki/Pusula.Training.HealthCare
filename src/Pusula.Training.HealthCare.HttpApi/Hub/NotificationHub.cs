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

    public async Task JoinGroup()
    {
        var userId = Context.UserIdentifier;
        if (CurrentUser.IsInRole(HealthCareRoles.Doctor))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, CurrentUser.Id.ToString());
            var a = CurrentUser.Id.ToString();
        } else if (CurrentUser.IsInRole(HealthCareRoles.RadyologyTechnician))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, HealthCareRoles.RadyologyTechnician);
        } else
        {
            Console.WriteLine(
                $"User role does not match the provided roles {HealthCareRoles.Doctor} or {HealthCareRoles.RadyologyTechnician}"
            );
        }
    }

    public async Task SendNotificationToDoctor(string message, Guid doctorId)
    {
        try
        {
            var a = doctorId.ToString();

            await Clients.Group(doctorId.ToString()).SendAsync("ReceiveNotification", message);

            Console.WriteLine("Notification sent successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error sending notification to doctor: {ex.Message}");
            await Clients.Caller.SendAsync("ReceiveNotification", "Failed to send notification to doctor.");
        }
    }
}