using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public interface IInteractable 
    {
        bool IsInteractable { get; }
        void Interact(Interactor interactor);     
        void InteractorExit(Interactor interactor);
    }
}
