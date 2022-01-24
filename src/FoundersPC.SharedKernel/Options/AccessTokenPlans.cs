namespace FoundersPC.SharedKernel.Options;

public class AccessTokenPlan
{
    public ulong AddSeconds { get; set; }
    public uint RequestLimitInSeconds { get; set; }
}

public class AccessTokenPlans
{
    public AccessTokenPlan Personal { get; set; } = default!;
    public AccessTokenPlan ProPlan { get; set; } = default!;
    public AccessTokenPlan Unlimited { get; set; } = default!;
}