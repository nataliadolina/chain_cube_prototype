using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RainbowCube : CubeBase
{
    protected override void OnCollisionWithOtherCube(Cube otherSimpleCube)
    {
        otherSimpleCube.Score *= 2;
        Destroy(gameObject);
    }

    protected override void OnPushInternal()
    {
        StartCoroutine(WaitToDestroyRaibowCube());
    }

    private IEnumerator WaitToDestroyRaibowCube()
    {
        yield return new WaitForSeconds(0.2f);
        while (rb.velocity.magnitude > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
        yield return null;
    }

    public class Factory : PlaceholderFactory<RainbowCube>
    {
       
    }
}
