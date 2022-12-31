using System.Collections;
using UnityEngine;

public class hyperlink : MonoBehaviour
{
    public void openURL(string link){
        Application.OpenURL(link);
    }
}
