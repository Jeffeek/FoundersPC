#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using FoundersPC.API.Domain.Entities.Hardware;

#endregion

namespace HardwareApi.Tests.MockAbstractions.DataCreation
{
    public static class HardwareCreation
    {
        public static IEnumerable<Producer> CreateProducers() =>
            Enumerable.Range(0, 1000)
                      .Select(x => new Producer
                                   {
                                       Country = CreationExtensions.GetRandomString(0, 8),
                                       FoundationDate =
                                           CreationExtensions.RandomInt(-100, 100) > 0
                                               ? new DateTime(CreationExtensions.RandomInt(1970, 2021),
                                                              CreationExtensions.RandomInt(1, 11),
                                                              CreationExtensions.RandomInt(1, 28))
                                               : null,
                                       FullName =
                                           CreationExtensions.GetRandomString(0, CreationExtensions.RandomInt(5, 10)),
                                       ShortName = CreationExtensions.RandomInt(-100, 100) > 0
                                                       ? CreationExtensions.GetRandomString()
                                                       : null,
                                       Website = CreationExtensions.RandomInt(-100, 100) > 0
                                                     ? $"https://{CreationExtensions.GetRandomString()}.com/"
                                                     : null
                                   });

        public static IEnumerable<Case> CreateCases(IEnumerable<Producer> producers)
        {
            var enumerable = producers as Producer[] ?? producers.ToArray();

            return Enumerable.Range(0, 1000)
                             .Select(_ => new Case
                                          {
                                              Color = CreationExtensions.GetRandomString(),
                                              Depth = CreationExtensions.GetRandomNullableInt(),
                                              Height = CreationExtensions.GetRandomNullableInt(),
                                              Material = CreationExtensions.GetRandomString(),
                                              MaxMotherboardSize = CreationExtensions.GetRandomString(),
                                              Title = CreationExtensions.GetRandomString(),
                                              TransparentWindow =
                                                  CreationExtensions.GetRandomNullableInt() is not null,
                                              ProducerId = enumerable
                                                           .ElementAt(CreationExtensions
                                                                          .RandomInt(0, enumerable.Length))
                                                           .Id,
                                              Type = CreationExtensions.GetRandomString(),
                                              Weight = CreationExtensions.GetRandomNullableInt(),
                                              Width = CreationExtensions.GetRandomNullableInt(),
                                              WindowMaterial = CreationExtensions.GetRandomString()
                                          });
        }
    }
}