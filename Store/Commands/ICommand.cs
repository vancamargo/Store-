using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Commands
{
    public interface ICommand
    {
        void Validate();
    }
}
