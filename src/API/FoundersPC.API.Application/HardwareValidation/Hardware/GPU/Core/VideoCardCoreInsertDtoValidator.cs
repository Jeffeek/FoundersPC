#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.GPU.Core
{
    /// <summary>
    ///     Validator for <see cref="VideoCardCoreInsertDto"/>
    /// </summary>
    public class VideoCardCoreInsertDtoValidator : AbstractValidator<VideoCardCoreInsertDto>
    {
        public VideoCardCoreInsertDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ArchitectureTitle)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Codename)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.ConnectionInterface)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.DirectX)
                .MaximumLength(10);

            RuleFor(x => x.MaxResolution)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .Matches("\\d{3,5}x\\d{3,5}");

            RuleFor(x => x.TechProcess)
                .GreaterThan(5)
                .LessThanOrEqualTo(48);

            RuleFor(x => x.MonitorsSupport)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(24);
        }
    }
}