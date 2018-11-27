using UnityEngine;
using UnityEngine.UI;

public class OnCloseListener : MonoBehaviour
{
    /// <summary>
    /// Called when the Browser is closed.
    /// </summary>
    public void OnClose()
    {
        // Uncomment this if you set up Interop
        //BrowserJS.Warn("This warning was called from Unity!");

        // Randomize the background image color

        GameSystem.instance.ResetClearData();
    }
}