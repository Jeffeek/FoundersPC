﻿#region Using namespaces

using System;

#endregion

namespace FoundersPC.SharedKernel.Exceptions;

public class JobExecutionException : Exception
{
    public JobExecutionException(string message) : base(message) { }
}