using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class PaintBoardDepositArea : DepositAreaBase
    {
        public static PaintBoardDepositArea Instance { get; private set; }
        
        [field : SerializeField] public Transform PlayerPoint { get; private set; }
        [SerializeField] private Transform paintBoard;

        protected override void Awake()
        {
            Instance = this;
            paintBoard.localScale = Vector3.zero;
            base.Awake();
        }

        protected override void Unlock()
        {
            if (IsCompleted) return;
            base.Unlock();
            paintBoard.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); 
            GameManager.Instance.SetState(GameStateId.Painting);
        }
    }
}
