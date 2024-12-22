using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyRequests; 

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemWithNavigationPropertiesDto
{
    public RadiologyRequestItemDto RadiologyRequestItem { get; set; } = null!;
    public RadiologyExaminationDto RadiologyExamination { get; set; } = null!;
    public RadiologyRequestDto RadiologyRequest { get; set; } = null!;
    public RadiologyExaminationDocumentDto Document { get; set; } = null!;
    public ProtocolDto Protocol { get; set; } = null!;  
    public DepartmentDto Department { get; set; } = null!;  
    public DoctorDto Doctor { get; set; } = null!;  
    public PatientDto Patient { get; set; } = null!;  
}
