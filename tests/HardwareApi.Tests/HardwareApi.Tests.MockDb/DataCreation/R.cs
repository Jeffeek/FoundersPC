#region Using namespaces

using System;

#endregion

namespace HardwareApi.Tests.MockAbstractions.DataCreation
{
    public static class R
    {
        private static readonly Random Random;

        static R() => Random = new Random();

        public static int RandomInt(int min = Int32.MinValue, int max = Int32.MaxValue) => Random.Next(min, max);

        public static double RandomDouble(double min = Double.MinValue, double max = Double.MaxValue) => Random.NextDouble() * (max - min);

        public static int? GetRandomNullableInt(int min = 1, int max = 1000) => Random.Next(-100, 100) > 0 ? Random.Next(min, max) : null;

        public static string GetRandomString(int minLength = 3, int maxLength = 10) =>
            Guid.NewGuid()
                .ToString()
                .Substring(minLength, maxLength);

        public static T OneOf<T>(params T[] parameters) => parameters[RandomInt(0, parameters.Length)];
    }
}