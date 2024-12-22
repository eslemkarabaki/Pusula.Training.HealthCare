using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Medicines;

public class GetMedicinesInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}