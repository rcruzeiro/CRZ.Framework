using System;
using System.ComponentModel.DataAnnotations;
using CRZ.Framework.CQRS.Commands;
using CRZ.Framework.CQRS.Queries;

namespace CRZ.Framework.CQRS
{
    public static class CQRSExtensions
    {
        public static void Validate(this ICommand command, ValidationContext context = null)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            command.Validate(context);
        }

        public static void Validate(this IFilter filter, ValidationContext context = null)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            filter.Validate(context);
        }
    }
}
