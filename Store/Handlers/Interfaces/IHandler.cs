using Store.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
