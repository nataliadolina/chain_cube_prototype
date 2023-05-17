using Environment;
using System.Linq;
using UnityEngine;
using Zenject;


public abstract class CubeBase : MonoBehaviour
{
    [Inject]
    private FieldLimits fieldLimits;
    [Inject]
    protected CubeSpawner spawner;

    protected Rigidbody rb;
    protected float force;

    private int score;

    public int Score { get => score; set { score = value; OnScoreSet(value); } }

    [Inject]
    private void Construct(CubeSettings cubeSettings)
    {
        this.force = cubeSettings.Force;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        OnConstructInternal(cubeSettings);
    }

    protected virtual void OnConstructInternal(CubeSettings cubeSettings) { }

    protected virtual void OnCollisionWithOtherCube(Cube otherCube) { }

    protected virtual void OnPushInternal() { }

    protected virtual void OnScoreSet(int score) { }
    public void MoveX(float deltaX)
    {
        Vector3 newPosition = transform.position += Vector3.left * deltaX;
        transform.position = fieldLimits.ClampPosition(newPosition);
    }

    private void OnDestroy()
    {
        spawner.CubesOnScene.Remove(spawner.CubesOnScene.Where(x => x == this).FirstOrDefault());
    }

    public void Push()
    {
        spawner.CurrentCube = null;
        rb.isKinematic = false;
        rb.freezeRotation = true;
        rb.AddRelativeForce(transform.forward * 10, ForceMode.Impulse);
        OnPushInternal();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.freezeRotation = false;
        Cube otherSimpleCube = collision.collider.GetComponent<Cube>();
        if (otherSimpleCube == null || (CubeBase)otherSimpleCube == spawner.CurrentCube)
        {
            return;
        }

        OnCollisionWithOtherCube(otherSimpleCube);
    }
}
