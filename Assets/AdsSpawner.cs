using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsSpawner : MonoBehaviour
{
    [SerializeField] 
    private AdsBlock _blockPrefab;

    [SerializeField] 
    private float _interval = 0.5f;
    private AdsBlock _block;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (;;)
        {
            while (_block != null)
            {
                yield return null;
            }

            Instantiate();
            yield return new WaitForSeconds(_interval);
        }
    }
    
    private void Instantiate()
    {
        if (_blockPrefab == null || 
            _block != null)
        {
            return;
        }

        
        _block = Instantiate(_blockPrefab, transform).GetComponent<AdsBlock>();
        _block.transform.position = transform.position;
        _block.transform.rotation = transform.rotation;
    }
}
