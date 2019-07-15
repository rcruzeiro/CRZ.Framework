using System;

namespace CRZ.Framework.Domain
{
    public static class CommandExtensions
    {
        public static void Validate(this ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            command.Validate();
        }
    }
}
