using System.Windows.Forms;

namespace UnityFramework
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DialogParameters
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    public class FileDialog
    {
        //Open file dialog
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] DialogParameters ofn);

        public static bool OpenFileName([In, Out] DialogParameters ofn)
        {
            return GetOpenFileName(ofn);
        }

        //Save file dialog
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] DialogParameters ofn);

        public static bool SaveFileName([In, Out] DialogParameters ofn)
        {
            return GetSaveFileName(ofn);
        }
    }

    /// <summary>
    /// Use this to create a windows file dialog, mode is open or save.
    /// </summary>
    public class Dialog
    {
        /// <summary>
        /// 创建原生的FileBrowserDialog
        /// </summary>
        /// <param name="_open"></param>
        /// <param name="_save"></param>
        /// <param name="_filePath">文件路径</param>
        public static void CreateDialogWindow(bool _open, bool _save, ref string _filePath)
        {
            DialogParameters dialogParas = new DialogParameters();
            dialogParas.structSize = Marshal.SizeOf(dialogParas);

            dialogParas.initialDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //"Picture Files(*.jpg,*.png,*.bmp)\0*.jpg;*.png;*.bmp";
            // dialogParas.filter = "All Files"; 
            dialogParas.filter = "Picture Files(*.jpg,*.png,*.bmp)\0*.jpg;*.png;*.bmp"; 
            dialogParas.file = new string(new char[256]);
            dialogParas.maxFile = dialogParas.file.Length;
            dialogParas.fileTitle = new string(new char[64]);
            dialogParas.maxFileTitle = dialogParas.fileTitle.Length;
            dialogParas.title = "Select File";
            dialogParas.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

            if (_open)
            {
                if (FileDialog.GetOpenFileName(dialogParas))
                {
                    _filePath = dialogParas.file;
                }
            }
            if (_save)
            {
                if (FileDialog.GetSaveFileName(dialogParas))
                {
                    _filePath = dialogParas.file;
                }
            }
        }
        
        /// <summary>
        /// 得到选择的文件夹路径，仅在需要的时候调用一次
        /// </summary>
        /// <returns></returns>
        public static string GetFolderPath()
        {
            string folderPath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择";
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folderPath = fbd.SelectedPath.Replace(@"\", "/");
            }

            return folderPath;
        }
    }
}