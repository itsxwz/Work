using System.IO;

namespace UnityFramework.Example
{
    using UnityEngine;

    /// <summary>
    /// Texture2PDF的测试脚本
    /// </summary>
    public class Example_Texture2PDF : MonoBehaviour
    {
        [SerializeField] Texture2D m_Tex2D;
        string m_FilePath;

        void Awake()
        {
            m_FilePath = Application.streamingAssetsPath + "/Texture2PDF";
            if (!Directory.Exists(m_FilePath))
            {
                Directory.CreateDirectory(m_FilePath);
            }
        }

        void Start()
        {
            Texture2PDF.SaveTexture2PDF(m_Tex2D, m_FilePath + "/Save.PDF", "png");

            Vector2 range = new Vector2(Screen.width, Screen.height);
            StartCoroutine(Texture2PDF.ScreenShot2Pdf(m_FilePath, "ScreenShot.pdf",
                Vector2.zero, range));
        }
    }
}