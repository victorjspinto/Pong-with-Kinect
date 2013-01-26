using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TutorialPong.Objects.Interfaces
{
    interface IMove : IObject
    {
        Vector2 Direction { get; set; }
        float Velocity { get; set; }
    }
}
