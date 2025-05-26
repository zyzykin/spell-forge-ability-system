using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpellForge.Scripts.Presentation
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private Button restartButton;

        private void Awake() => HideWinPanel();

        private void OnEnable()
        {
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartButtonClicked);
            }

            GameStateManager.GameOverEvent += ShowWinPanel;
        }

        private void OnDisable()
        {
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            }

            GameStateManager.GameOverEvent -= ShowWinPanel;
        }

        private void OnRestartButtonClicked()
        {
            SceneRestarted();
        }

        private void SceneRestarted()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }

        private void ShowWinPanel()
        {
            if (winPanel != null)
            {
                winPanel.alpha = 1f;
                winPanel.interactable = true;
                winPanel.blocksRaycasts = true;
            }
        }

        private void HideWinPanel()
        {
            if (winPanel != null)
            {
                winPanel.alpha = 0f;
                winPanel.interactable = false;
                winPanel.blocksRaycasts = false;
            }
        }
    }
}