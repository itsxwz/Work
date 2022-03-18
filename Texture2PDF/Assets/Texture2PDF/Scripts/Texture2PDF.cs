namespace UnityFramework
{
    using System.Collections;
    using System.IO;
    using UnityEngine;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    /// <summary>
    /// 通过itextsharp.dll实现图片转PDF
    /// 1.图片必须是Read/Write Enabled = True
    /// 2.图片format必须是RGBA32
    /// </summary>
    public static class Texture2PDF
    {
        /// <summary>
        /// 保存图片为PDF
        /// 规格：A4
        /// </summary>
        /// <param name="texture2D"></param>
        /// <param name="path"></param>
        public static void SaveTexture2PDF(Texture2D texture2D, string path, string fileType)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            byte[] bytes = null;
            fileType = fileType.ToLower();
            switch (fileType)
            {
                case "png":
                    bytes = texture2D.EncodeToPNG();
                    break;
                
                case "exr":
                    bytes = texture2D.EncodeToEXR();
                    break;
            
                case "jpg":
                    bytes = texture2D.EncodeToJPG();
                    break;
                
                case "tga":
                    bytes = texture2D.EncodeToTGA();
                    break;
            }

            if (bytes == null)
                return;
            
            MemoryStream ms = new MemoryStream(bytes);

            Document doc = new Document(PageSize.A4);
            FileStream fs = new FileStream(path, FileMode.CreateNew);
            PdfWriter.GetInstance(doc, fs);

            Image image = Image.GetInstance(ms);
            if (image.Height > PageSize.A4.Height || image.Width > PageSize.A4.Width)
            {
                image.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
            }

            image.Alignment = Element.ALIGN_MIDDLE;

            doc.Open();
            doc.Add(image);
            doc.Close();
            
            ms.Close();
            fs.Dispose();
            fs.Close();
        }

        /// <summary>
        /// 屏幕截图保存为PDF
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="startPos">屏幕起始点</param>
        /// <param name="range">截图范围</param>
        /// <returns></returns>
        public static IEnumerator ScreenShot2Pdf(string filePath, string fileName, Vector2 startPos, Vector2 range)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            yield return new WaitForEndOfFrame();

            string fullPath = filePath + "/" + fileName + ".pdf";
            Texture2D tex = new Texture2D((int) range.x, (int) range.y, TextureFormat.RGBA32, true);
            tex.ReadPixels(new Rect(startPos.x, startPos.y, range.x, range.y), 0, 0, false);
            tex.Apply();

            SaveTexture2PDF(tex, fullPath, "PNG");
        }
    }
}