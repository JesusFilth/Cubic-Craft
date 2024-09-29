using System;
using System.Collections;
using Reflex.Attributes;
using Source.Scripts.Animations;
using Source.Scripts.Input;
using Source.Scripts.Views.Game.InterfaceStateMashine;
using UnityEngine;

namespace Source.Scripts.Characters.Player
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class Player : MonoBehaviour, IWallet, IPlayerPosition, IPlayerStats
    {
        private const float ImmortalityTime = 3f;

        [SerializeField] private MeleeAttack _meleeAttack;
        [SerializeField] private GameObject _healthView;
        [SerializeField] private GameObject _immortalEffect;

        private PlayerStats _stats;
        private CharacterAnimation _animation;
        private Transform _transform;
        private Wallet _wallet = new Wallet();
        private Coroutine _immortaling;

        [Inject] private IPlayerInput _playerInput;
        [Inject] private UIStateMashine _gameUI;

        public event Action<int> CoinChanged;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
            _animation = GetComponent<CharacterAnimation>();

            _transform = transform;
        }

        private void Start()
        {
            CoinChanged?.Invoke(_wallet.Coin);
        }

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (Exception ex)
            {
                enabled = false;
                throw ex;
            }

            _stats.Died += Die;
        }

        private void OnDisable()
        {
            _stats.Died -= Die;

            if (_immortaling != null)
            {
                StopCoroutine(_immortaling);
                _immortaling = null;
            }
        }

        private void Update()
        {
            if (_playerInput.IsAttack())
            {
                Attack();
            }
        }

        public PlayerStats GetStats() => _stats;

        public void Resurrect()
        {
            const int Life = 1;

            _stats.AddLife(Life);
            _stats.AddHealth(_stats.MaxHealth);
            _healthView.SetActive(true);
            _animation.ToIdel();

            if (_immortaling == null)
                _immortaling = StartCoroutine(Immortaling());
        }

        public void SetPosition(Transform point)
        {
            if (_transform == null)
                _transform = transform;

            _transform.position = point.position;
        }

        public Transform GetPosition() => _transform;

        public int GetCoin() => _wallet.Coin;

        public void AddCoin(int coin)
        {
            _wallet.AddCoin(coin);
            CoinChanged?.Invoke(_wallet.Coin);
        }

        private void Validate()
        {
            if (_meleeAttack == null)
                throw new ArgumentNullException(nameof(_meleeAttack));

            if (_healthView == null)
                throw new ArgumentNullException(nameof(_healthView));

            if (_immortalEffect == null)
                throw new ArgumentNullException(nameof(_immortalEffect));
        }

        private void Attack()
        {
            if (_meleeAttack.TryHit(_stats.Damage))
            {
                _animation.ToAttack();
            }
        }

        private void Die()
        {
            _animation.ToDie();
            _gameUI.EnterIn<RewardLifeUIState>();
        }

        private IEnumerator Immortaling()
        {
            const float Health = 1;

            CreateImmortalEffect();

            float currentTime = 0;

            while (currentTime < ImmortalityTime)
            {
                _stats.AddHealth(Health);
                currentTime += Time.deltaTime;

                yield return null;
            }

            _immortaling = null;
        }

        private void CreateImmortalEffect()
        {
            GameObject effect = Instantiate(_immortalEffect, gameObject.transform);
            effect.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Destroy(effect, ImmortalityTime);
        }
    }
}