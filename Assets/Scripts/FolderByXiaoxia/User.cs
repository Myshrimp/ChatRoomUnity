using FishNet.Object;
using UnityEngine;

public class User : NetworkBehaviour
{

    public override void OnStartClient()
    {
        if(base.IsOwner)
        {
            Debug.Log("A new client");
            int id=gameObject.GetInstanceID();
            Register(id);
        }
    }

    [ServerRpc(RequireOwnership =false)]
    public void Register(int id)
    {
        Center_Input.Message message = new Center_Input.Message();
        message.message = "A new user has come joined the chat " + id.ToString();
        Debug.Log(message.message);
        message.isAi = false;
        message.show_description = false;
        Center_Input.instance.WelcomeMessage(message);
    }
}
