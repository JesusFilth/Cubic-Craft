using System.Collections;
using Reflex.Attributes;
using Source.Scripts.Characters;
using Source.Scripts.Characters.Player;
using Source.Scripts.Sounds;
using Source.Scripts.Views.Game.InterfaceStateMashine;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class FloorLava : MonoBehaviour
    {
        private const float TimeScale = 0.1f;
        private const int Life = 1;

        [SerializeField] private Transform _startPoint;
        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private float _delayHideUI = 1;
        [SerializeField] private float _durationUI = 1.5f;

        private Coroutine _showing;

        [Inject] private UIStateMashine _gameUI;
        [Inject] private IFlooLavaSound _sound;

        private void Awake()
        {
            GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
        }

        private void OnDisable()
        {
            if (_showing != null)
            {
                StopCoroutine(_showing);
                _showing = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                StartShow(player);
            }
        }

        private IEnumerator Showing(Player player)
        {
            Time.timeScale = TimeScale;

            _gameUI.EnterIn<LavaUIState>();

            yield return new WaitForSeconds(_duration);

            if (player.gameObject.TryGetComponent(out CharacterController controller))
            {
                controller.enabled = false;
                player.gameObject.transform.position = _startPoint.position;
                controller.enabled = true;
            }

            Time.timeScale = 1;

            yield return new WaitForSeconds(_delayHideUI);
            _gameUI.EnterIn<GameLevelUIState>();

            if (player.gameObject.TryGetComponent(out Stats stat))
            {
                stat.AddLife(-Life);
                stat.AddHealth(stat.MaxHealth);
            }

            _showing = null;
        }

        private void StartShow(Player player)
        {
            _sound.Play();

            if (_showing == null)
                _showing = StartCoroutine(Showing(player));
        }
    }
}
