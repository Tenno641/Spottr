using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Api.Common;

public static class SessionExtensions
{
    public static (bool, List<SessionTypes>) ToSessionsTypes(this List<string>? types)
    {
        List<SessionTypes> result = [];
        
        if (types is null)
            return (false, result);

        foreach (String type in types)
        {
            if (!Enum.TryParse(type, out SessionTypes sessionTyp))
                return (false, result);
        }
        
        return  (true, result);
    }
}