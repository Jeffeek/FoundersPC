#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using FoundersPC.SharedKernel.Extensions;

#endregion

namespace FoundersPC.SharedKernel.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.") =>
        Errors = new Dictionary<string, string[]>();

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this() =>
        failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ForEach(e => Errors.Add(e.Key, e.ToArray()));

    public IDictionary<string, string[]> Errors { get; }
}