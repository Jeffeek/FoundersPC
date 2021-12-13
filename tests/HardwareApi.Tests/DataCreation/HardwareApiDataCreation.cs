#region Using namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AutoMapper.Internal;
using Bogus;
using Bogus.Extensions;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Enums;

#endregion

namespace HardwareApi.Tests.DataCreation;

public static class HardwareApiDataCreation
{
    private static readonly Faker<Producer> ProducerFaker;

    private static readonly Faker<Case> CasesFaker;

    static HardwareApiDataCreation()
    {
        //ProducerFaker = new Faker<Producer>()
        //                .RuleFor(x => x.FoundationDate, faker => faker.Date.Between(new(1900, 1, 1), DateTime.Now))
        //                .RuleFor(x => x.Website, faker => faker.Internet.Url())
        //                .RuleFor(x => x.ShortName,
        //                         faker => faker.Lorem.Text()
        //                                       .OrNull(faker))
        //                .RuleFor(x => x.FullName, faker => faker.Name.FullName())
        //                .RuleFor(x => x.Country.Name, faker => faker.Address.Country());

        //CasesFaker = new Faker<Case>()
        //             //.RuleFor(x => x.HardwareType, HardwareType.Case)
        //             .RuleFor(x => x.Width,
        //                      faker => faker.Random.Int(50, 400)
        //                                    .OrNull(faker))
        //             .RuleFor(x => x.Weight,
        //                      faker => faker.Random.Double(1, 50)
        //                                    .OrNull(faker, 0.2f))
        //             .RuleFor(x => x.Height,
        //                      faker => faker.Random.Int(50, 200)
        //                                    .OrNull(faker, 0.3f))
        //             .RuleFor(x => x.Depth,
        //                      faker => faker.Random.Int(20, 100)
        //                                    .OrNull(faker, 0.2f))
        //             .RuleFor(x => x.Color,
        //                      faker => faker.PickRandom(typeof(Color).GetProperties()
        //                                                             .Where(x => x.IsPublic())
        //                                                             .Select(x => x.Name)))
        //             .RuleFor(x => x.Material, faker => faker.Commerce.ProductMaterial())
        //             .RuleFor(x => x.WindowMaterial, faker => faker.Commerce.ProductMaterial())
        //             .RuleFor(x => x.TransparentWindow, faker => faker.Random.Bool(0.65f))
        //             .RuleFor(x => x.ProducerId, faker => faker.Random.Int(0, 10))
        //             .RuleFor(x => x.Title, faker => faker.Commerce.ProductName())
        //             .RuleFor(x => x.Producer, ProducerFaker.Generate());
    }

    public static IEnumerable<Producer> CreateProducers() => ProducerFaker.GenerateForever();

    public static IEnumerable<Case> CreateCases() => CasesFaker.GenerateForever();
}