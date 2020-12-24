using System;

namespace Sample.Scripts
{
    public interface IButtonView
    {
        event Action Click;
    }
}