using System;
using System.ComponentModel.DataAnnotations;

namespace CRZ.Framework.Domain
{
    public static class CommandExtensions
    {
        public static void Validate(this ICommand command, ValidationContext context = null)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            command.Validate(context);
        }
    }
}
