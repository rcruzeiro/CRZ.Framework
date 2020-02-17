using System;

namespace CRZ.Framework.Web.Http
{
    public class CircuitBreakerOptions
    {
        public int EventsAllowedBeforeBreaking { get; }

        public TimeSpan DurationOfBreak { get; }

        public CircuitBreakerOptions(int eventsAllowedBeforeBreaking, TimeSpan durationOfBreak)
        {
            if (eventsAllowedBeforeBreaking == default) throw new ArgumentException(nameof(eventsAllowedBeforeBreaking));
            if (durationOfBreak == default) throw new ArgumentException(nameof(durationOfBreak));

            EventsAllowedBeforeBreaking = eventsAllowedBeforeBreaking;
            DurationOfBreak = durationOfBreak;
        }
    }
}
