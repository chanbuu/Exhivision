using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Crosstales.FB;

public class PhotoSet : MonoBehaviour {
	private string filePath;
	private SpriteRenderer photoRenderer;

	private string localFileName; // 拡張子込み

	private void Start(){
		this.photoRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
		this.Initialize();
	}

	private void Initialize(){
		if(this.photoRenderer == null) return;
		this.localFileName = this.photoRenderer.name + ".png";
		Debug.Log("try download: "+Application.persistentDataPath+this.localFileName);
		if(File.Exists(Application.persistentDataPath+this.localFileName)){
			this.filePath = Application.persistentDataPath+localFileName;
			setPhoto();
		}
	
	}
	private void OnCollisionEnter(Collision coll){
		Debug.Log("CollisionEnter");
	}

	private void OnTriggerEnter(Collider coll){
		Debug.Log("Trigger Enter");
	}

	public void OnRayHit(){
		StartCoroutine(getFile());
		
	}
	
	private IEnumerator getFile(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		yield return new WaitForEndOfFrame();
		this.filePath = FileBrowser.OpenSingleFile("Open Files", "", "");
		yield return new WaitForFixedUpdate();
		this.saveFile(this.filePath);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		setPhoto();
		yield return null;
	}

	private void setPhoto(){
		if(string.IsNullOrEmpty(this.filePath)) return;
		if(this.photoRenderer == null) return;
		
		BinaryReader bin = new BinaryReader(new FileStream(this.filePath,FileMode.Open,FileAccess.Read));
		byte[] by = bin.ReadBytes((int)bin.BaseStream.Length);
		bin.Close();
		int pos = 16, width = 0, height = 0;
		for (int i = 0; i < 4; i++) width  = width  * 256 + by[pos++];
		for (int i = 0; i < 4; i++) height = height * 256 + by[pos++];
		Texture2D texture = new Texture2D(width, height);
		texture.LoadImage(by);
		
		this.photoRenderer.sprite = Sprite.Create(texture,new Rect(0,0,width,height),Vector2.zero);

	}


	private void saveFile(string _path){
		// pathを指定の場所に.
		if(File.Exists(_path)){
			File.Copy(_path,Application.persistentDataPath+this.localFileName,true);
		} 
	}
}
