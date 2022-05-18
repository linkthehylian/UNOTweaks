using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tree.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UNO;
using UNO.GamePlay;

public class SplashScreen : MonoBehaviour
{
	private int m_movieIndex;

	public GameObject PS4ScreenPlane;

	public GameObject X1ScreenPlane;

	public string[] MovieList;

	public GameObject m_epilepsyWarning;

	private List<MovieTexture> m_movieList = new List<MovieTexture>();

	private bool m_showVideo;

	private void Start()
	{
		CardAnimationDatas.InitializeDatas();
		SimpleSingleton<UplayPC>.Instance.Attach();
		SimpleSingleton<WinAppMutexLock>.Instance.Attach();
		initGamePlayLogic();
		StartCoroutine(begin());
	}

	private IEnumerator begin()
	{
		yield return new WaitForEndOfFrame();
		string url = "Menu/3D_Assets/MainMenu/SpawnManager";
		ResourceRequest resourceRequest = Resources.LoadAsync(url);
		yield return resourceRequest;
		GameObject obj = resourceRequest.asset as GameObject;
		Singleton<PreloadSystem>.Instance.cacheGameObject(obj, url);
		SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
		//QualitySettings.vSyncCount = 0;
		//Application.targetFrameRate = -1;
		CardSkinManager.vSyncTog = PlayerPrefs.GetInt("vsync") == 1;
		if (PlayerPrefs.GetInt("vsync") == 1)
		{
			QualitySettings.vSyncCount = 1;
			Application.targetFrameRate = 60;
		}
		else
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = -1;
		}
		CardSkinManager.skinTog = PlayerPrefs.GetInt("skins") == 1;
		if (PlayerPrefs.GetInt("skins") == 1)
			CardSkinManager.skinTog = true;
		else
			CardSkinManager.skinTog = false;
	}

	private void playMovies()
	{
		loadMovieList();
		m_showVideo = true;
		playMovieTexture(m_movieIndex);
	}

	private void OnGUI()
	{
		if (!m_showVideo)
		{
			return;
		}
		if (!m_movieList[m_movieIndex].isPlaying)
		{
			if (m_movieIndex == m_movieList.Count - 1)
			{
				m_showVideo = false;
				onMovieFinished();
				return;
			}
			m_movieIndex++;
			playMovieTexture(m_movieIndex);
		}
		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), m_movieList[m_movieIndex], ScaleMode.StretchToFill, alphaBlend: false, 0f);
	}

	private void loadMovieList()
	{
		string[] movieList = MovieList;
		foreach (string str in movieList)
		{
			MovieTexture item = Resources.Load("Video_PCX1/" + str) as MovieTexture;
			m_movieList.Add(item);
		}
	}

	private void playMovieTexture(int _idx)
	{
		m_movieList[_idx].Play();
		AudioSource component = GetComponent<AudioSource>();
		component.clip = m_movieList[_idx].audioClip;
		component.Play();
		if (_idx == 0)
		{
			SimpleSingleton<AudioManager>.Instance.playEvent("Play_Sfx_UNO_Logo");
		}
	}

	private string GenerateMoviePath(string _str)
	{
		return Path.Combine(Application.streamingAssetsPath, "Movies//intro//" + _str + ".mp4");
	}

	private void onMovieFinished()
	{
		StartCoroutine(end());
	}

	private IEnumerator end()
	{
		m_epilepsyWarning.GetComponent<epliepsyController>().ShowVideoWarn();
		yield return new WaitForSeconds(5f);
		SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
	}

	private void initGamePlayLogic()
	{
		RootScript.RunInBackground(_value: false, "SplashScreen.Start");
		SimpleSingleton<MultiplayerManager>.Instance.Attach();
		SimpleSingleton<ProductManager>.Instance.Attach();
		Singleton<ViewManager>.Instance.Initialize();
		Tree.UI.UIRoot.Instance.UICamera.gameObject.SetActive(value: false);
		SimpleSingleton<CoroutineManager>.Instance.Initialize();
		SimpleSingleton<UserManagerAdapter>.Instance.Attach();
		SimpleSingleton<AudioManager>.Instance.Attach();
		SimpleSingleton<AudioManager>.Instance.registerSoundBank();
	}
}
