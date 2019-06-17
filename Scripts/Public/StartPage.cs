using UnityEngine;
using System.Collections;

public class StartPage : MonoBehaviour {
    
    public Texture StartButton;

    Rect ScreenRect;

	// Use this for initialization
	void Start () {
        StartButton = GetComponent<GUITexture>().texture;
      //  ScreenRect.x = Screen.width / 2 - guiTexture.pixelInset.width / 2;
       // ScreenRect.y = Screen.height / 2 - guiTexture.pixelInset.height / 2;

      //  ScreenRect.width = guiTexture.pixelInset.width;
       // ScreenRect.height = guiTexture.pixelInset.height;

        //guiTexture.pixelInset = ScreenRect;
	}

    void OnMouseDown()
    {
        Application.LoadLevel(1);
    }
	// Update is called once per frame
	void Update () {
        Screen.lockCursor = false;
	}
}
