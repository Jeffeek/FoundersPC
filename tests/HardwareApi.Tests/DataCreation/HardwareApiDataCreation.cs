#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Domain.Entities.Identity.Users;
using HardwareType = FoundersPC.Domain.Enums.HardwareType;

#endregion

namespace HardwareApi.Tests.DataCreation;

public static class HardwareApiDataCreation
{
    private static readonly Faker<Producer> ProducerFaker =
        new Faker<Producer>()
            .RuleFor(x => x.FoundationDate, faker => faker.Date.Between(new(1900, 1, 1), DateTime.Now))
            .RuleFor(x => x.Website, faker => faker.Internet.Url())
            .RuleFor(x => x.ShortName, faker => faker.Lorem.Text().OrNull(faker))
            .RuleFor(x => x.FullName, faker => faker.Name.FullName());

    private static readonly Faker<CaseMetadata> CasesMetadataFaker =
        new Faker<CaseMetadata>()
            .RuleFor(x => x.Id, Int32.MinValue)
            .RuleFor(x => x.Width, faker => faker.Random.Double(50, 400).OrNull(faker))
            .RuleFor(x => x.Weight, faker => faker.Random.Double(1, 50).OrNull(faker, 0.2f))
            .RuleFor(x => x.Height, faker => faker.Random.Double(50, 200).OrNull(faker, 0.3f))
            .RuleFor(x => x.Depth, faker => faker.Random.Double(20, 100).OrNull(faker, 0.2f))
            .RuleFor(x => x.TransparentWindow, faker => faker.Random.Bool(0.65f))
            .RuleFor(x => x.Title, faker => faker.Commerce.ProductName())
            .RuleFor(x => x.Producer, ProducerFaker.Generate());

    private static readonly Faker<Case> CasesFaker =
        new Faker<Case>()
            .RuleFor(x => x.Id, Int32.MinValue)
            .RuleFor(x => x.HardwareTypeId, (int)HardwareType.Case)
            .RuleFor(x => x.Metadata, CasesMetadataFaker.Generate());

    public static IEnumerable<Producer> CreateProducers() => ProducerFaker.GenerateForever();

    public static IEnumerable<Case> CreateCases() => CasesFaker.GenerateForever();

    public static ApplicationRole CreateSystemRole() =>
        new()
        {
            Id = 1,
            Name = "System"
        };

    public static ApplicationUser CreateSystemUser(ApplicationRole role) =>
        new()
        {
            RoleId = role.Id,
            ApplicationRole = role,
            Login = "System",
            PasswordHash = "",
            SendMessageOnApiRequest = false,
            SendMessageOnEntrance = false,
            IsActive = true,
            IsBlocked = false,
            Email = ""
        };

    public static IEnumerable<FoundersPC.Domain.Entities.Hardware.HardwareType> CreateHardwareTypes() =>
        Enum.GetValues<HardwareType>()
            .Select(x => new FoundersPC.Domain.Entities.Hardware.HardwareType
                         {
                             Id = (int)x,
                             Name = x.ToString()
                         })
            .ToList();
}