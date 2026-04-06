namespace GymManagement.Contracts.Subscriptions;

public record CreateSubscriptionRequest(Guid AdminId, string SubscriptionType);