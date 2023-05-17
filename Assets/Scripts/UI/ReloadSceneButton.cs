using UnityEngine.SceneManagement;

public class ReloadSceneButton : ButtonBase
{
    protected override void OnClickInternal()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CubeDynamicSettings.MaxTwoPower = 1;
    }
}
