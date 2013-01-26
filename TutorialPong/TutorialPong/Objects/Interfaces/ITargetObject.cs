using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorialPong.Objects.Interfaces
{
    interface ITarget : IObject
    {
        Int32 Health { get; set; }
        Int32 MaxHealth { get; set; }
    }
}
