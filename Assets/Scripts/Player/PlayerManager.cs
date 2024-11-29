using System;
using System.Threading.Tasks;
using Core.Cutscene;
using Core.Game;
using Core.Score;
using Cysharp.Threading.Tasks;
using Ui.Assessment;
using Ui.Menu;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpawn[] spawnPlayers;
        private PlayerController playerRef;
        private GameObject playerObject;
        
        private void Awake()
        {
            playerRef = null;
            playerObject = null;
            
            MenuViewManager.gameInitialized += GameInitialized;
            PlayerEvents.OnPlayerStateChange += OnPlayerStateChange;
        }



        private void OnDestroy()
        {
            MenuViewManager.gameInitialized -= GameInitialized;
            PlayerEvents.OnPlayerStateChange -= OnPlayerStateChange;
            RemovePlayer();
        }
        
        private async void GameInitialized()
        {
            if (playerRef) playerRef = null;
            if (playerObject) Destroy(playerObject);

            await UniTask.WaitUntil(() => !playerRef && !playerObject);
            
            LoadPlayer(ref playerRef, ref playerObject);
        }

        private void RemovePlayer()
        {
            if (playerObject) Destroy(playerObject);
            if (playerRef) playerRef = null;
        }

        private void LoadPlayer(ref PlayerController player, ref GameObject playerObject)
        {
            var playerPrefab = Resources.Load<GameObject>("Player");
            playerObject = Instantiate(playerPrefab);
            player = playerObject.GetComponent<PlayerController>();
            
            DontDestroyOnLoad(playerObject);
            
            SetPlayerSpawnPoint(SceneName.StreetScene);
        }
        
        private void OnPlayerStateChange(PlayerState state)
        {
            if (!playerObject || !playerRef) return;
            playerRef.SetPlayerState(state);
        }

        private void SetPlayerSpawnPoint(SceneName scene)
        {
            foreach (var playerSpawn in spawnPlayers)
            {
                if (playerSpawn.sceneName == scene)
                {
                    playerObject.transform.position = playerSpawn.spawnTransform.position;
                    playerObject.transform.rotation = playerSpawn.spawnTransform.rotation;
                    Physics.SyncTransforms();
                    break;
                }
            }
        }
    }

    [Serializable]
    public struct PlayerSpawn
    {
        public SceneName sceneName;
        public Transform spawnTransform;
    }
}