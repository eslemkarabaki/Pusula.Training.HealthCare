using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressCreateDto : EntityDto<Guid>
{
    [Required]
    [NotEmptyGuid]
    public Guid DistrictId { get; set; }

    public DistrictDto District { get; set; } = new();

    [Required]
    [StringLength(AddressConsts.TitleMaxLength)]
    public string AddressTitle { get; set; } = null!;

    [Required]
    [StringLength(AddressConsts.AddressMaxLength)]
    public string AddressLine { get; set; } = null!;
}