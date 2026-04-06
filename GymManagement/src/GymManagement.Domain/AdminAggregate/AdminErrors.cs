using ErrorOr;
using Microsoft.VisualBasic;

namespace GymManagement.Domain.AdminAggregate;

public static class AdminErrors
{
    public static Error SubscriptionAlreadyExists = Error.Conflict("Subscription already exists");
}