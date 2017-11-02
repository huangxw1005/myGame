using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;


public class ResourceManager : Singleton<ResourceManager> {

	public void Init() {
		Debug.Log("ResourceManager:Init");

	}

	private AssetBundle GetAssetBundle(string assetBundleName) {
		if (UpdateManager.Instance.LocalFiles.ContainsKey(assetBundleName)) {
			return AssetBundleManager.GetAssetBundle(assetBundleName);
		} else {
			// parent
			string parent = assetBundleName.Substring(0, assetBundleName.LastIndexOf('/'));

			if (UpdateManager.Instance.LocalFiles.ContainsKey(parent)) {
				return AssetBundleManager.GetAssetBundle(parent);
			}
		}
		return null;
	}


	public Object LoadAsset(string assetName) {
		string assetBundleName = assetName.ToLower();
		AssetBundle assetBundle = GetAssetBundle(assetBundleName);
		if (assetBundle != null) {
			int pos = assetBundleName.LastIndexOf("/");
			string name = assetBundleName.Substring (pos + 1); 
			Object asset = assetBundle.LoadAsset(name);
			return asset;
		} else {
			return Resources.Load(assetBundleName);
		}
	}
	public Sprite LoadSprite(string assetName) {
		string assetBundleName = assetName.ToLower();
		AssetBundle assetBundle = GetAssetBundle(assetBundleName);
		if (assetBundle != null) {
			int pos = assetBundleName.LastIndexOf("/");
			return assetBundle.LoadAsset<Sprite>(assetBundleName.Substring(pos+1));
		} else {
			return Resources.Load<Sprite>(assetName);
		}
	}
	public Sprite LoadPackSprite(string assetName) {
		string assetBundleName = assetName.ToLower();
		AssetBundle assetBundle = GetAssetBundle(assetBundleName);
		if (assetBundle != null) {
			int pos = assetBundleName.LastIndexOf("/");
			return assetBundle.LoadAsset<GameObject>(assetBundleName.Substring(pos+1)).GetComponent<SpriteRenderer>().sprite;
		} else {
			return Resources.Load<GameObject>(assetName).GetComponent<SpriteRenderer>().sprite;
		}
	}

	public void UnloadAsset(string assetName) {
		string assetBundleName = assetName.ToLower();

		if (UpdateManager.Instance.LocalFiles.ContainsKey(assetBundleName)) {
			AssetBundleManager.UnloadAssetBundle(assetBundleName);
		} else {
			// parent
			string parent = assetBundleName.Substring(0, assetBundleName.LastIndexOf('/'));

			if (UpdateManager.Instance.LocalFiles.ContainsKey(parent)) {
				AssetBundleManager.UnloadAssetBundle(parent);
			}
		}
	}

	public static long GetFileSize(string filename) {
		if (!File.Exists(filename)) {
			Debug.LogFormat("GetFileSize: {0} not Exist!", filename);
			return 0;
		}

		FileStream fs = new FileStream(filename, FileMode.Open);
		long length = fs.Length;
		fs.Close();
		return length;
	}
	public static string GetFileHash(string filename) {
		if (!File.Exists(filename)) {
			Debug.LogFormat("GetFileHash: {0} not Exist!", filename);
			return null;
		}

		FileStream fs = new FileStream(filename, FileMode.Open);
		byte[] data = new byte[fs.Length];
		fs.Read (data, 0, (int)fs.Length);
		Hash128 hash = Hash128.Parse (data.ToString ());
		fs.Close();
		return hash.ToString();
	}
	public static string GetFileMD5(string filename) {
		if (!File.Exists(filename)) {
			Debug.LogFormat("GetFileMD5: {0} not Exist!", filename);
			return null;
		}

		FileStream fs = new FileStream(filename, FileMode.Open);
		byte[] data = new byte[fs.Length];
		fs.Read (data, 0, (int)fs.Length);
		fs.Close();
		MD5 md5 = new MD5CryptoServiceProvider ();
		byte[] result = md5.ComputeHash (data);
		string filemd5 = "";
		foreach (byte b in result) {
			filemd5 += System.Convert.ToString (b, 16);
		}
		return filemd5;
	}
}