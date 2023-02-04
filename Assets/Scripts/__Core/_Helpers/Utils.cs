using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils {

    public static Vector3 RandomBetween(Vector3 min, Vector3 max)
    {
        Vector3 vector = new Vector3
        {
            x = UnityEngine.Random.Range(min.x, max.x),
            y = UnityEngine.Random.Range(min.y, max.y),
            z = UnityEngine.Random.Range(min.z, max.z)
        };
        return vector;
    }

    static Camera mainCamera;
    public static Collider GetMouseCollider(LayerMask layerMask)
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return null;
        }
        return hit.collider;
    }
    public static Vector3 GetMouseCollisionPoint(LayerMask layerMask)
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return Vector3.zero;
        }
        return hit.point;
    }

    //TODO fix method name
    public static float AngleToPoint(Vector3 point1, Vector3 point2)
	{
		return Mathf.Atan2(point2.y - point1.y, point2.x - point1.x) * Mathf.Rad2Deg;
	}

	public static void ReloadCurrentScene() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

//	public static float AngleTo(this Vector3 this_, Vector3 to) {
//		Vector3 direction = to - this_;
//		float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
//		if (angle < 0f) {
//			angle += 360f;
//		}
//		return angle;
//	}


//	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
//		return Mathf.Atan2(a.z - b.z, a.x - b.x) * Mathf.Rad2Deg;
//	}

	//JSON array converter 1
	//YouObject[] objects = JsonHelper.getJsonArray<YouObject> (jsonString);
	public class JsonHelper {
		public static T[] getJsonArray<T>(string json) {
			string newJson = "{ \"array\": " + json + "}";
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
			return wrapper.array;
		}

		[System.Serializable]
		private class Wrapper<T> {
			public T[] array = {}; //modifiquei aqui pra parar de dar warning no console
		}
	}

	//JSON array converter 2
	//string stringMagica = WrapToClass (hs_post.text.ToString (), "hs");
	//hsA = JsonUtility.FromJson<HighscoreA>(stringMagica);
//	public static string WrapToClass(string source, string topClass){ 
//		return string.Format("{{ \"{0}\": {1}}}", topClass, source); 
//	}
//	[System.Serializable] public class HighscoreA
//	{
//		public Highscore[] hs;
//	}
//
//	[System.Serializable] public class Highscore
//	{
//		public string name;
//		public int score;
//	}
//	public HighscoreA hsA;
//
//	public Highscore[] hsArray;

	//another alternative: Newtonsoft.Json
	//PlayerStats[] statArray = JsonConvert.DeserializeObject<PlayerStats[]>(json);


//	this needs a change in php scripts, to use POSTs instead of GETs
//	string hash = Encrypt(id_.ToString() + playerScore + secretKey);
//
//	WWWForm form = new WWWForm();
//	form.AddField("OPT", WWW.EscapeURL(35.ToString()));
//	form.AddField("ID", WWW.EscapeURL(id_.ToString()));
//	form.AddField("HASH", hash);
//
//	UnityWebRequest download = UnityWebRequest.Post("https://cavernao.com/games/GGJ18/GGJ18_GP.php", form);
//	yield return download.SendWebRequest();
//
//	if (download.isNetworkError || download.isHttpError) {
//		print( "Error downloading: " + download.error );
//	} else {
//		retrievedPosString = download.downloadHandler.text;
//		print (retrievedPosString);
//		//			retrievedPosString = hs_post.text.ToString();
//		retrievedPosStringSplit = retrievedPosString .Split(' ');
//		int.TryParse(retrievedPosStringSplit[0], out posData.currentMatch);
//		int.TryParse(retrievedPosStringSplit[1], out posData.totalMatches);
//		if (posData.currentMatch > posData.totalMatches) {
//			posData.currentMatch = posData.totalMatches;
//		}
//	}

	public static string MD5Encrypt(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

    public static string SHA256Encrypt(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        System.Security.Cryptography.SHA256CryptoServiceProvider sha = new System.Security.Cryptography.SHA256CryptoServiceProvider();
        byte[] hashBytes = sha.ComputeHash(bytes);

        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }




    public static string GetEnumName<T>(T myEnum)
    {
        return Enum.GetName(typeof(T), myEnum);
    }






    public static void SafeInvoke(this Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke<T>(this Action<T> action, T obj)
    {
        if (action != null)
        {
            action.Invoke(obj);
        }
    }

    public static void SafeInvoke<T, U>(this Action<T, U> action, T t, U u)
    {
        if (action != null)
        {
            action.Invoke(t, u);
        }
    }

    public static void SafeInvoke<T, U, V>(this Action<T, U, V> action, T t, U u, V v)
    {
        if (action != null)
        {
            action.Invoke(t, u, v);
        }
    }

    public static void SafeInvoke<T, U, V, X>(this Action<T, U, V, X> action, T t, U u, V v, X x)
    {
        if (action != null)
        {
            action.Invoke(t, u, v, x);
        }
    }



    public static void WaitAndAct(this MonoBehaviour mono, float waitTime, Action endAction)
    {
        mono.StartCoroutine(WaitAndAct(waitTime, endAction));
    }

    private static IEnumerator WaitAndAct(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action.Invoke();
    }


    public static int Fib(int aIndex)
    {
        int n1 = 0;
        int n2 = 1;
        for (int i = 0; i < aIndex; i++)
        {
            int tmp = n1 + n2;
            n1 = n2;
            n2 = tmp;
        }
        return n1;
    }





    public static float GetPercentageBetweenPoints(float current, float min, float max)
    {
        return (current - min) / (max - min);
    }



    public static bool IsConnectedToInternet()
    {
        if (//NeedInternetConnection &&
            Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        return true;
    }


    //rigid.velocity = Vector3.MoveTowards(Vector3.zero, targetMove.transform.position - base.transform.position, 1f) * moveForce* moveForceArenaMultiplier * moveForceAbilityMultiplier;


}
