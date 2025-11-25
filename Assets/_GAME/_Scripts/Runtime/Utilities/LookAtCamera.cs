using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;
        private Camera MainCamera => _camera == null ? _camera = Camera.main : _camera;
        
        private void Update()
        {
            if (MainCamera is null) 
                return;
            
            transform.LookAt(transform.position + MainCamera.transform.forward);
        }
    }
}
