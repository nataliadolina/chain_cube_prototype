using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CloseButton : ButtonBase
{
    [SerializeField]
    private GameObject panel;

    protected override void OnClickInternal()
    {
        StartCoroutine(WaitToClosePanel());
    }

    private IEnumerator WaitToClosePanel()
    {
        yield return new WaitForSeconds(0.2f);
        panel.SetActive(false);
        touchController.gameObject.SetActive(true);
    }
}
