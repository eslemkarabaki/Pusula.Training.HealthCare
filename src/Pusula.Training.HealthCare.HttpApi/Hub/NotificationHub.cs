using Microsoft.AspNetCore.SignalR;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Users;

namespace Pusula.Training.HealthCare.Hub
{
    public class NotificationHub : AbpHub
    {
        private readonly ICurrentUser _currentUser;

        public NotificationHub(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
         
        public async Task JoinGroup()
        {
            var userId = Context.UserIdentifier;
            if (_currentUser.IsInRole(HealthCareRoles.Doctor))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, _currentUser.Id.ToString());
                var a = _currentUser.Id.ToString();

            }
            else if (_currentUser.IsInRole(HealthCareRoles.RadyologyTechnician))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, HealthCareRoles.RadyologyTechnician);
            }
            else
            { 
                Console.WriteLine($"User role does not match the provided roles {HealthCareRoles.Doctor} or {HealthCareRoles.RadyologyTechnician}");
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

        public async Task SendNotificationToRadiologyTechnicians(string message)
        { 
            await Clients.Group(HealthCareRoles.RadyologyTechnician).SendAsync("ReceiveNotification", message);
        }

        public async Task GetMessage(string message)
        {
            Console.WriteLine("Message Received: " + message);
        }
    }
}
