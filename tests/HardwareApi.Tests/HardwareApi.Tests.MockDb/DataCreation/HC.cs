#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Memory;

#endregion

namespace HardwareApi.Tests.MockAbstractions.DataCreation
{
    public static class HC
    {
        public static int Count = 10;

        public static ProducerEntity CreateProducer() =>
            new()
            {
                Country = R.GetRandomString(0, 8),
                FoundationDate =
                    R.RandomInt(-100, 100) > 0
                        ? new DateTime(R.RandomInt(1970, 2021),
                                       R.RandomInt(1, 11),
                                       R.RandomInt(1, 28))
                        : null,
                FullName =
                    R.GetRandomString(0, R.RandomInt(5, 10)),
                ShortName = R.RandomInt(-100, 100) > 0
                                ? R.GetRandomString()
                                : null,
                Website = R.RandomInt(-100, 100) > 0
                              ? $"https://{R.GetRandomString()}.com/"
                              : null
            };

        public static IEnumerable<ProducerEntity> CreateProducers() =>
            Enumerable.Range(0, Count)
                      .Select(_ => CreateProducer());

        public static CaseEntity CreateCase(IEnumerable<ProducerEntity> producers)
        {
            var enumerable = producers as ProducerEntity[] ?? producers.ToArray();

            return new CaseEntity
                   {
                       Color = R.GetRandomString(),
                       Depth = R.GetRandomNullableInt(),
                       Height = R.GetRandomNullableInt(),
                       Material = R.GetRandomString(),
                       MaxMotherboardSize = R.GetRandomString(),
                       Title = R.GetRandomString(),
                       TransparentWindow =
                           R.GetRandomNullableInt() is not null,
                       ProducerId = enumerable
                                    .ElementAt(R
                                                   .RandomInt(0, enumerable.Length))
                                    .Id,
                       Type = R.GetRandomString(),
                       Weight = R.GetRandomNullableInt(),
                       Width = R.GetRandomNullableInt(),
                       WindowMaterial = R.GetRandomString()
                   };
        }

        public static IEnumerable<CaseEntity> CreateCases(IEnumerable<ProducerEntity> producers)
        {
            var enumerable = producers as ProducerEntity[] ?? producers.ToArray();

            return Enumerable.Range(0, 10)
                             .Select(_ => CreateCase(enumerable));
        }

        public static HardDriveDiskEntity CreateHDD(IEnumerable<ProducerEntity> producers)
        {
            var enumerable = producers as ProducerEntity[] ?? producers.ToArray();

            return new HardDriveDiskEntity
                   {
                       BufferSize = R.RandomInt(30, 5000),
                       Factor = R.OneOf(2.5, 3.5),
                       HeadSpeed = R.OneOf(5400, 7200),
                       Interface = R.OneOf("SATA", "M.2"),
                       Noise = R.RandomInt(20, 100),
                       ProducerEntity = R.OneOf(enumerable),
                       Title = R.GetRandomString(),
                       Volume = R.RandomInt(60, 10000)
                   };
        }

        public static IEnumerable<HardDriveDiskEntity> CreateHDDs(IEnumerable<ProducerEntity> producers)
        {
            var enumerable = producers as ProducerEntity[] ?? producers.ToArray();

            return Enumerable.Range(0, Count)
                             .Select(_ => CreateHDD(enumerable));
        }
    }
}