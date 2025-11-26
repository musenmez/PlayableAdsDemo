using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PaintingState : GameStateBase
    {
        public override void Enter()
        {
            Player.Instance.Movement.DisableMovement();
            Player.Instance.PaintingAreaHandler.SetupPaintingState();
            UIManager.Instance.Joystick.gameObject.SetActive(false);
            UIManager.Instance.HidePanel(PanelId.Currency);
            UIManager.Instance.ShowPanel(PanelId.Painting);
            CameraManager.Instance.ActivateCamera(CameraId.PaintBoard, 1f);
        }
    }
}
