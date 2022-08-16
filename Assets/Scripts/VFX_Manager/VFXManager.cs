using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        COIN,
        RESPAWN
    }

    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXByType(VFXType vfxType,Vector3 position)
    {
        foreach(var i in vfxSetup)
        {
            if (i.vfxType == vfxType)
            {
                Debug.Log(i.vfxType.ToString());
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item.gameObject, 9f);
                
            }
        }
    }

}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}