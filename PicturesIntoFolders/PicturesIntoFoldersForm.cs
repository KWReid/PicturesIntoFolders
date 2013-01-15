using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicturesIntoFolders
{
  public partial class PicturesIntoFoldersForm : Form
  {
    public PicturesIntoFoldersForm()
    {
      InitializeComponent();
    }

    private string m_PicturesRootFolder = string.Empty;
    private string PicturesRootFolder
    {
      get
      {
        if (string.IsNullOrEmpty(m_PicturesRootFolder))
        {
          m_PicturesRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);  // "C:\\Users\\kwreid\\Desktop\\TargetImageFolder\\"; // "getthisfromconfigfile";
        }
        return m_PicturesRootFolder;
      }
      set
      {
        m_PicturesRootFolder = value;
      }
    }

    private void SelectFilesButton_Click(object sender, EventArgs e)
    {
      if (FileBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        for (int i = 0; i < FileBrowserDialog.FileNames.Length; i++)
        {
          ProcessFile(FileBrowserDialog.FileNames[i]);
        }

      }
    }

    private void ProcessFile(string filename)
    {
      string dateFromImageFile = string.Empty;

      try
      {
        DateTime d = getDateFromImageFile(filename);
        dateFromImageFile = d.ToString("yyyy-MM-dd");
      }
      catch (Exception e)
      {
        dateFromImageFile = "[No Date]";
      }

      string targetFolderName = PicturesRootFolder + "\\" + dateFromImageFile + "\\";

      if (System.IO.Directory.Exists(targetFolderName) == false)
      {
        System.IO.Directory.CreateDirectory(targetFolderName);
      }

      System.IO.File.Move(filename, targetFolderName + System.IO.Path.GetFileName(filename));
      AddLogMessage("Moved '" + System.IO.Path.GetFileName(filename) + "' to '" + targetFolderName + "'");
    }

    private DateTime getDateFromImageFile(string FileName)
    {
      System.Drawing.Image img = null;
      string sdate = string.Empty;

      using (System.IO.FileStream stream = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
      {
        img = Image.FromStream(stream);

        sdate = System.Text.Encoding.UTF8.GetString(img.GetPropertyItem(0x0132).Value).Trim();

        string secondhalf = sdate.Substring(sdate.IndexOf(" "), (sdate.Length - sdate.IndexOf(" ")));

        string firsthalf = sdate.Substring(0, 10);
        firsthalf = firsthalf.Replace(":", "-");

        sdate = firsthalf + secondhalf;
      }

      return DateTime.Parse(sdate);
    }

    private void AddLogMessage(string message)
    {
      m_LogTextbox.AppendText(message + Environment.NewLine);

    }

    private void m_DropTargetPanel_DragDrop(object sender, DragEventArgs e)
    {
      string[] FileNames = (string[])e.Data.GetData("FileDrop");

      for (int i = 0; i < FileNames.Length; i++)
      {
        ProcessFile(FileNames[i]);
      }
    }

    private void m_DropTargetPanel_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent("FileDrop"))
      {
        e.Effect = DragDropEffects.Copy;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }
  }
}
