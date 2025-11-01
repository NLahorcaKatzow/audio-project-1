using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    private void OnDisable() {
        AudioManager.GetInstance().ExitUI();
    }
    private void OnEnable() {
        AudioManager.GetInstance().EnterUI();
    }

}
