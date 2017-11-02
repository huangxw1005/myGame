using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 
/// 2D ͼ�����ɶ�ӦԤ�Ƽ��ű���.
/// 
/// �������: 
/// ������ܶ�̬���� Sprite �������ͼ������, ��Ϊ��ɢ��Сͼ������Ҫ���Ϊͼ���� Unity3D �ֽ�ͼ���Ĵ�������, ������Ҫ��Сͼ����Ϣ��ŵ�Ԥ�Ƽ���.
/// 
/// ʹ�÷���������: 
/// 1.�޸�ԴĿ¼��Ŀ��Ŀ¼Ϊ�Լ�ʹ�õ�Ŀ¼;
/// 2.��Сͼ��ŵ�ԴĿ¼��;
/// 3.�ڲ˵������ Hammerc/2D/MakeSpritePrefabs ������Ŀ��Ŀ¼�����ɶ�Ӧ��Ԥ�Ƽ�;
/// 4.ͨ����ȡ���ɵ�Ԥ�Ƽ��� sprite ���󼴿��ڳ�����ʹ��Сͼ.
/// 
/// </summary>
public class MakeSpritePrefabsScript
{
	/// <summary>
	/// Assets Ŀ¼�µ�СͼƬĿ¼, ������Ŀ¼������ͼƬ�ļ�������д���.
	/// </summary>
	private const string ORIGIN_DIR = "\\Arts";

	/// <summary>
	/// Assets Ŀ¼�µ�СͼԤ�Ƽ����ɵ�Ŀ��Ŀ¼, ע���Ŀ¼�²�Ҫ���������Դ, ÿ������ʱ������ո�Ŀ¼�µ������ļ�.
	/// </summary>
	private const string TARGET_DIR = "\\Resources\\Sprite";

	/// <summary>
	/// ���ƶ�Ŀ¼�µ�ԭʼͼƬһ��һ����� Prefab ��������Ϸ�����ж�ȡָ����ͼƬ.
	/// </summary>
	[MenuItem("UI/MakeSpritePrefabs")]
	private static void MakeSpritePrefabs()
	{
		EditorUtility.DisplayProgressBar("Make Sprite Prefabs", "Please wait...", 1);

		string targetDir = Application.dataPath + TARGET_DIR;
		//ɾ��Ŀ��Ŀ¼
		if(Directory.Exists(targetDir))
		    Directory.Delete(targetDir, true);
		if(File.Exists(targetDir + ".meta"))
		    File.Delete(targetDir + ".meta");
		//�����յ�Ŀ��Ŀ¼
		if(!Directory.Exists(targetDir))
		    Directory.CreateDirectory(targetDir);

		//��ȡԴĿ¼������ͼƬ��Դ������
		string originDir = Application.dataPath + ORIGIN_DIR;
		DirectoryInfo originDirInfo = new DirectoryInfo(originDir);
		MakeSpritePrefabsProcess(originDirInfo.GetFiles("*.jpg", SearchOption.AllDirectories), targetDir);
		MakeSpritePrefabsProcess(originDirInfo.GetFiles("*.png", SearchOption.AllDirectories), targetDir);

		EditorUtility.ClearProgressBar();
	}

	static private void MakeSpritePrefabsProcess(FileInfo[] files, string targetDir) 
	{
		foreach(FileInfo file in files) {

		    string allPath = file.FullName;
		    string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
		    //������ͼ
		    Sprite sprite = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite)) as Sprite;
		    //����������ͼ�� GameObject ����
		    GameObject go = new GameObject(sprite.name);
		    go.AddComponent<SpriteRenderer>().sprite = sprite;
		    //��ȡĿ��·��
		    string targetPath = assetPath.Replace("Assets" + ORIGIN_DIR + "\\", "");
		    //ȥ����׺
		    targetPath = targetPath.Substring(0, targetPath.IndexOf("."));
		    //�õ�����·��
		    targetPath = targetDir + "\\" + targetPath + ".prefab";
		    //�õ�Ӧ�õ�ǰĿ¼��·��
		    string prefabPath = targetPath.Substring(targetPath.IndexOf("Assets"));
		    //����Ŀ¼
		    Directory.CreateDirectory(prefabPath.Substring(0, prefabPath.LastIndexOf("\\")));
		    //����Ԥ�Ƽ�
		    PrefabUtility.CreatePrefab(prefabPath.Replace("\\", "/"), go);
		    //���ٶ���
		    GameObject.DestroyImmediate(go);
		}
	}
}