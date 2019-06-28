using System.ComponentModel.DataAnnotations;

namespace CRZ.Framework.CQRS.Commands
{
    public interface ICommand : IValidatableObject
    { }
}
