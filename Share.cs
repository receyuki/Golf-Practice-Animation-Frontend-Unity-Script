using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class Share : MonoBehaviour
{
    public string m_shareMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShareViaMessage ()
    {
        if( NPBinding.Sharing.IsMailServiceAvailable()){
            // Create composer
            MessageShareComposer    _composer    = new MessageShareComposer();
            _composer.Body                        = "test";
            NPBinding.Sharing.ShowView(_composer, FinishedSharing);
        }
    }

    public void ShareViaShareSheet ()
    {
        // Create new instance and populate fields
        ShareSheet _shareSheet  = new ShareSheet(); 
        _shareSheet.Text        = m_shareMessage;

        // On iPad, popover view is used to show share sheet. So we need to set its position
        NPBinding.UI.SetPopoverPointAtLastTouchPosition();
        // Attaching screenshot here
        _shareSheet.AttachScreenShot();
        // Show composer
        NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
    }



    private void FinishedSharing (eShareResult _result)
    {
        Debug.Log("Finished sharing");
        Debug.Log("Share Result = " + _result);
    }
}
