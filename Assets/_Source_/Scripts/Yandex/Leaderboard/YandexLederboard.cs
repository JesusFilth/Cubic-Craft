using System;
using System.Collections.Generic;
using System.Linq;
using Agava.YandexGames;
using Reflex.Attributes;
using Source.Scripts.Core.Localization;
using UnityEngine;

namespace Source.Scripts.Yandex.Leaderboard
{
    public class YandexLederboard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const int MaxPlayers = 6;

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new ();

        [SerializeField] private LederboardView _leaderboadrView;

        [Inject] private LocalizationTranslate _localizationTranslate;

        private void Awake()
        {
            Initialize();
            Fill();
        }

        private void OnValidate()
        {
            if (_leaderboadrView == null)
                throw new ArgumentNullException(nameof(_leaderboadrView));
        }

        private void Fill()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();

            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = _localizationTranslate.GetAnonymousName();

                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }

                _leaderboardPlayers.OrderByDescending(player => player.Rank).ToList();
                _leaderboadrView.Construct(_leaderboardPlayers.Take(MaxPlayers).ToList());
            });
        }

        private void Initialize()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}