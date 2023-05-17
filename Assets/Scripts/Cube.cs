using UnityEngine;
using Zenject;

public class Cube : CubeBase
{
    [Inject]
    private CreateRainbowCubeButton createRainbowCubeButton;

    [Inject]
    private CubeText cubeText;

    [Inject]
    private ScoreText scoreText;

    [Inject]
    private CubeRendererController renderController;

    private int twoPower;
    private float jumpForce;
    private float halfJumpForce;

    private System.Random random = new System.Random();

    private int TwoPower
    {
        get => twoPower;
        set
        {
            twoPower = value;
            renderController.SetColor(value);
        }
    }

    protected override void OnConstructInternal(CubeSettings cubeSettings)
    {
        cubeText.Init();
        renderController.Init();
        jumpForce = cubeSettings.JumpForce;
        halfJumpForce = jumpForce / 2;

        twoPower = random.Next(1, CubeDynamicSettings.MaxTwoPower + 1);
        Score = (int)Mathf.Pow(2, twoPower);
    }

    protected override void OnScoreSet(int score)
    {
        cubeText.SetText(score.ToString());
        TwoPower = (int)Mathf.Log(score, 2);
        if (twoPower > CubeDynamicSettings.MaxTwoPower)
        {
            CubeDynamicSettings.MaxTwoPower = twoPower;
            createRainbowCubeButton.NumGrowth++;
        }
    }

    protected override void OnCollisionWithOtherCube(Cube otherSimpleCube)
    {
        if (otherSimpleCube.Score == Score)
        {
            Destroy(otherSimpleCube.gameObject);
            Score *= 2;

            rb.AddForce(CalculateForce(otherSimpleCube), ForceMode.Impulse);
            scoreText.AddScore(Score);
        }
    }

    private Vector3 CalculateForce(Cube otherSimpleCube)
    {
        CubeBase cube = spawner.GetNeighborWithTheSameScore(Score, this, otherSimpleCube);
        if (cube == null)
        {
            return Vector3.up * jumpForce;
        }

        transform.LookAt(cube.transform);
        return Vector3.up * jumpForce + transform.forward * halfJumpForce;
    }

    public class Factory : PlaceholderFactory<Cube>
    {
        
    }
}
