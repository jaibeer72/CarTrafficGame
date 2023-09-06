using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesControlller : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;
    // Start is called before the first frame update
    public GameObject[] collectables;
    private int _currentCollectableOnBoard = 0;
    private int maxCollectableOnBoard;
    public int PlayGroundRadius = 10;

    private IEnumerator _coroutine;
    void Start()
    {
        for(int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }

        _coroutine = SpwanRandomeLyOnTheLevel();
        StartCoroutine(_coroutine);

        maxCollectableOnBoard = gameConfig.configData.MaxCollectablesOnBoard;

        CollectablesEvents.PlayerCollected.AddListener(OnPlayerCollected);
    }

    private void OnPlayerCollected(GameObject arg0)
    {
        // Find collatable in the array and set it to false
        // set the current collectable on board to -1
        for(int i = 0; i < collectables.Length; i++)
        {
            if (collectables[i] == arg0)
            {
                collectables[i].SetActive(false);
                _currentCollectableOnBoard--;
                break;
            }
        }
    }

    IEnumerator SpwanRandomeLyOnTheLevel()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            if (_currentCollectableOnBoard >= maxCollectableOnBoard)
            {
                yield return new WaitForSeconds(5f);
            }
            else
            {
                int randomeIndex = Random.Range(0, collectables.Length);
                if (!collectables[randomeIndex].activeInHierarchy)
                {
                    Vector2 pointInsideUnitCircle = Random.insideUnitCircle * PlayGroundRadius;
                    collectables[randomeIndex].transform.position = new Vector3(pointInsideUnitCircle.x , this.transform.position.y , pointInsideUnitCircle.y);
                    collectables[randomeIndex].SetActive(true);
                    _currentCollectableOnBoard++;
                }
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
