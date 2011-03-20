using System;

namespace SoftLattice.Common
{
    public interface IInteractionModel
    {
        void Clicked();
        event EventHandler Closed;
    }
}