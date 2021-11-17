using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntroManager : MonoBehaviour
{
    #region lifecycle
    private void Awake()
    {
        StartCoroutine(JoinLobby());
    }
    #endregion


    IEnumerator JoinLobby()
    {
        yield return new WaitForSeconds(1.0f);
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Lobby);
        Debug.Log("로비로 이동합니다");
    }

}
