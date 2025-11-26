using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerPaintingAreaHandler : MonoBehaviour
    {
        [SerializeField] private Transform rotationBody;
        
        public void SetupPaintingState()
        {
            var target = PaintBoardDepositArea.Instance.PlayerPoint;
            transform.DOMove(target.position, 0.15f).SetEase(Ease.Linear); 
            rotationBody.DORotateQuaternion(target.rotation, 0.15f).SetEase(Ease.Linear); 
        }
    }
}
