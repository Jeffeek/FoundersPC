﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AutoMapper.Internal;
using Bogus;
using Bogus.Extensions;
using FoundersPC.API.Domain.Entities;

#endregion

namespace HardwareApi.Tests.MockDb.DataCreation
{
    public static class HardwareApiDataCreation
    {
        private static readonly Faker<ProducerEntity> ProducerFaker;

        private static readonly Faker<CaseEntity> CasesFaker;

        static HardwareApiDataCreation()
        {
            ProducerFaker = new Faker<ProducerEntity>()
                            .RuleFor(x => x.FoundationDate, faker => faker.Date.Between(new DateTime(1900, 1, 1), DateTime.Now))
                            .RuleFor(x => x.Website, faker => faker.Internet.Url())
                            .RuleFor(x => x.ShortName,
                                     faker => faker.Lorem.Text()
                                                   .OrNull(faker))
                            .RuleFor(x => x.FullName, faker => faker.Name.FullName())
                            .RuleFor(x => x.Country, faker => faker.Address.Country());

            CasesFaker = new Faker<CaseEntity>()
                         .RuleFor(x => x.Type, faker => faker.Random.Word())
                         .RuleFor(x => x.Width,
                                  faker => faker.Random.Int(50, 400)
                                                .OrNull(faker))
                         .RuleFor(x => x.Weight,
                                  faker => faker.Random.Double(1, 50)
                                                .OrNull(faker, 0.2f))
                         .RuleFor(x => x.Height,
                                  faker => faker.Random.Int(50, 200)
                                                .OrNull(faker, 0.3f))
                         .RuleFor(x => x.Depth,
                                  faker => faker.Random.Int(20, 100)
                                                .OrNull(faker, 0.2f))
                         .RuleFor(x => x.Color,
                                  faker => faker.PickRandom(typeof(Color).GetProperties()
                                                                         .Where(x => x.IsPublic())
                                                                         .Select(x => x.Name)))
                         .RuleFor(x => x.Material, faker => faker.Commerce.ProductMaterial())
                         .RuleFor(x => x.MaxMotherboardSize, faker => faker.PickRandomParam("20+24", "20+4+4", "24"))
                         .RuleFor(x => x.WindowMaterial, faker => faker.Commerce.ProductMaterial())
                         .RuleFor(x => x.TransparentWindow, faker => faker.Random.Bool(0.65f))
                         .RuleFor(x => x.ProducerId, faker => faker.Random.Int(0, 10))
                         .RuleFor(x => x.Title, faker => faker.Commerce.ProductName())
                         .RuleFor(x => x.ProducerEntity, ProducerFaker.Generate());
        }

        public static IEnumerable<ProducerEntity> CreateProducers() => ProducerFaker.GenerateForever();

        public static IEnumerable<CaseEntity> CreateCases() => CasesFaker.GenerateForever();
    }
}