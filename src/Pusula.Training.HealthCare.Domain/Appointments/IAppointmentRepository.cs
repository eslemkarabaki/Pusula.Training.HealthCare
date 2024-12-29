using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Appointments;

public interface IAppointmentRepository:IRepository<Appointment, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null, 
        DateTime? startTime=null, DateTime? endTime =null,
        string? note = null, ICollection<EnumAppointmentStatus>? statuses=null,  
        Guid? appointmentTypeId=null, Guid? departmentId=null, 
        Guid? doctorId =null, Guid? patientId=null, 
        CancellationToken cancellationToken=default);

    Task<AppointmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

    Task<List<AppointmentWithNavigationProperties>> GetDoctorAppointmentListWithNavigationPropertiesAsync(
        Guid doctorId,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ICollection<EnumAppointmentStatus>? statuses = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        DateTime? startTime = null, DateTime? endTime = null,
        string? note = null, ICollection<EnumAppointmentStatus>? statuses = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        string? sorting=null, int maxResultCount=int.MaxValue,
        int skipCount=0, CancellationToken cancellationToken=default);

    Task<List<Appointment>> GetListAsync(
        string? filterText = null,
        DateTime? startTime = null, DateTime? endTime = null,
        string? note = null, ICollection<EnumAppointmentStatus>? statuses = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        string? sorting =null, int maxResultCount = int.MaxValue,
        int skipCount = 0, CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
        string? filterText = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? note = null,
         ICollection<EnumAppointmentStatus>? statuses = null,
        Guid? appointmentTypeId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        Guid? patientId = null,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountForDoctorAppointmentListAsync(
        Guid doctorId,
        DateTime? startTime = null,
        DateTime? endTime = null,
        ICollection<EnumAppointmentStatus>? statuses = null,
        CancellationToken cancellationToken = default
    );
}
