using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class FinalState : GameStateBase
    {
        private Coroutine _finalCo;
        
        private const float TRANSITION_DELAY = 1.25f;
        private const float LOAD_DELAY = 1f;
        
        public override void Enter()
        {
            StopCoroutine(_finalCo);
            _finalCo = GameManager.Instance.StartCoroutine(FinalCo());
        }

        public override void Exit()
        {
            base.Exit();
            if (_finalCo != null)
                StopCoroutine(_finalCo);
        }

        private IEnumerator FinalCo()
        {
            GameManager.Instance.OnLevelCompleted.Invoke();
            UIManager.Instance.HidePanel(PanelId.Painting);
            yield return new WaitForSeconds(TRANSITION_DELAY);
            
            UIManager.Instance.ShowPanel(PanelId.Transition);
            yield return new WaitForSeconds(LOAD_DELAY);
            
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
