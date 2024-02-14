using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class AndroidPermissionHandler : MonoBehaviour
{
    readonly string[] _permissions = { Permission.ExternalStorageRead, Permission.ExternalStorageWrite };
    
    bool _hasRequestedPermissions = false;

    // Start is called before the first frame update
    void Start()
    {
        CheckStoragePermissions();
    }

    // checks whether permissions are granted before or after asking
    public bool CheckStoragePermissions()
    {
        if(HasStoragePermissions())
        {
            return true;
        }

        // permissions not yet granted -> ask for permissions
        RequestStoragePermissions();
        
        return HasStoragePermissions();
    }

    // check permissions state
    bool HasStoragePermissions()
    {
        return Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) 
            && Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
    }

    // request permissions if not already asked
    void RequestStoragePermissions()
    {
        if (!_hasRequestedPermissions)
        {
            Permission.RequestUserPermissions(_permissions);
            _hasRequestedPermissions = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
