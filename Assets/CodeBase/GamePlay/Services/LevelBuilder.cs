using Common;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBuilder : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject m_PlayerHUDPrefab;
        [SerializeField] private GameObject m_LevelGUIPrefab;
        [SerializeField] private GameObject m_BackroundPrefab;

        [Header("Dependencies")]
        [SerializeField] private PlayerSpawner m_PlayerSpawner;
        [SerializeField] private LevelBoundary levelBoundary;
        [SerializeField] private LevelController levelController;


        private void Awake()
        {
            levelBoundary.Init();
            levelController.Init();

            Player player = m_PlayerSpawner.Spawn();

            player.Init();

            Instantiate (m_PlayerHUDPrefab);
            Instantiate (m_LevelGUIPrefab);

            GameObject backround = Instantiate(m_BackroundPrefab);
            backround.AddComponent<SyncTransform>().SetTarget(player.FollowCamera.transform);
        }

    }
}

