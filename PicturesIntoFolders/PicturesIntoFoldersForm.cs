using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PicturesIntoFolders
{
  public partial class PicturesIntoFoldersForm : Form
  {
    public PicturesIntoFoldersForm()
    {
      InitializeComponent();
    }

    // Note: See this great list of EXIF helper methods I got the following enum from:
    //   http://blog.crazybeavers.se/index.php/archive/reading-exif-with-extension-methods/
    public enum PropertyTag
    {
      ImageWidth = 0x100,
      ImageLength = 0x101,
      NBitsPerSample = 0x102,
      Compression = 0x103,
      PhotometricInterpretation = 0x106,
      Orientation = 0x112,
      SamplesPerPixel = 0x115,
      PlanarConfiguration = 0x11C,
      YCbCrSubSampling = 0x212,
      YCbCrPositioning = 0x213,
      XResolution = 0x11A,
      YResolution = 0x11B,
      ResolutionUnit = 0x128,
      StripOffsets = 0x111,
      RowsPerStrip = 0x116,
      StripByteCounts = 0x117,
      JPEGInterchangeFormat = 0x201,
      JPEGInterchangeFormatLength = 0x202,
      TransferFunction = 0x12D,
      WhitePoint = 0x13E,
      PrimaryChromaticities = 0x13F,
      YCbCrCoefficients = 0x211,
      ReferenceBlackWhite = 0x214,
      DateTime = 0x132,
      ImageDescription = 0x10E,
      Make = 0x10F,
      Model = 0x110,
      Software = 0x131,
      Artist = 0x13B,
      Copyright = 0x8298,
      ExifVersion = 0x9000,
      FlashpixVersion = 0xA000,
      ColorSpace = 0xA001,
      ComponentsConfiguration = 0x9101,
      CompressedBitsPerPixel = 0x9102,
      PixelXDimension = 0xA002,
      PixelYDimension = 0xA003,
      MakerNote = 0x927C,
      UserComment = 0x9286,
      RelatedSoundFile = 0xA004,
      DateTimeOriginal = 0x9003,
      DateTimeDigitized = 0x9004,
      SubSecTime = 0x9290,
      SubSecTimeOriginal = 0x9291,
      SubSecTimeDigitized = 0x9292,
      ImageUniqueID = 0xA420,
      ExposureTime = 0x829A,
      FNumber = 0x829D,
      ExposureProgram = 0x8822,
      SpectralSensitivity = 0x8824,
      ISOSpeedRatings = 0x8827,
      OECF = 0x8828,
      ShutterSpeedValue = 0x9201,
      ApertureValue = 0x9202,
      BrightnessValue = 0x9203,
      ExposureBiasValue = 0x9204,
      MaxApertureValue = 0x9205,
      SubjectDistance = 0x9206,
      MeteringMode = 0x9207,
      LightSource = 0x9208,
      Flash = 0x9209,
      FocalLength = 0x920A,
      SubjectArea = 0x9214,
      FlashEnergy = 0xA20B,
      SpatialFrequencyResponse = 0xA20C,
      FocalPlaneXResolution = 0xA20E,
      FocalPlaneYResolution = 0xA20F,
      FocalPlaneResolutionUnit = 0xA210,
      SubjectLocation = 0xA214,
      ExposureIndex = 0xA215,
      SensingMethod = 0xA217,
      FileSource = 0xA300,
      SceneType = 0xA301,
      CFAPattern = 0xA302,
      CustomRendered = 0xA401,
      ExposureMode = 0xA402,
      WhiteBalance = 0xA403,
      DigitalZoomRatio = 0xA404,
      FocalLengthIn35mmFilm = 0xA405,
      SceneCaptureType = 0xA406,
      GainControl = 0xA407,
      Contrast = 0xA408,
      Saturation = 0xA409,
      Sharpness = 0xA40A,
      DeviceSettingDescription = 0xA40B,
      SubjectDistanceRange = 0xA40C
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

    private string getDateFromFile(string filename)
    {
      string dateFromFile = string.Empty;

      switch (System.IO.Path.GetExtension(filename))
      {
        case ".jpg":
        case ".jpeg":
          DateTime d = getDateFromImageFile(filename);
          dateFromFile = d.ToString("yyyy-MM-dd");
          break;
        default:
          dateFromFile = getDateFromFilename(filename);
          break;
      }

      return dateFromFile;
    }

    // some examples I try to catch
    // VID_20210802_105406.mp4
    // PANO_20210725_151026.jpg
    // IMG_20210717_180720.jpg
    private string getDateFromFilename(string filename)
    {
      string dateFromFilename = string.Empty;

      string filenameWithDate_pattern = "[a-zA-Z]{3,}_\\d{8}_.*\\.[a-zA-z]+";            // using https://regex101.com/
      Match m = Regex.Match(filename, filenameWithDate_pattern, RegexOptions.IgnoreCase);
      if (m.Success)
      {
        // grab the 8 digit YYYYMMDD string in the middle of the filename as date
        string[] s = filename.Split('_');
        dateFromFilename = s[1].Substring(0, 4) + "-" + s[1].Substring(4, 2) + "-" + s[1].Substring(6, 2);
      }
      else 
      {
        dateFromFilename = "[No Date]";
      }
      
      return dateFromFilename;
    }

    private void ProcessFile(string filename)
    {
      string dateFromFile = string.Empty;

      try
      {
        dateFromFile = getDateFromFile(filename);
      }
      catch (Exception e)
      {
        // System.Diagnostics.Debug.Assert(false, "Failure retreiving a valid date from image file: '" + e.Message + "\n\n" + e.StackTrace.ToString());
        dateFromFile = "[No Date]";
      }

      string targetFolderName = PicturesRootFolder + "\\" + dateFromFile + "\\";

      if (System.IO.Directory.Exists(targetFolderName) == false)
      {
        System.IO.Directory.CreateDirectory(targetFolderName);
      }

      if (System.IO.File.Exists(targetFolderName + System.IO.Path.GetFileName(filename)))
      {
        AddLogMessage("! File already exists '" + System.IO.Path.GetFileName(filename) + "'");
        return;
      }

      try
      {
        System.IO.File.Move(filename, targetFolderName + System.IO.Path.GetFileName(filename));
        AddLogMessage("Moved '" + System.IO.Path.GetFileName(filename) + "' to '" + targetFolderName + "'");
      }
      catch (System.Exception ex)
      {
        AddLogMessage("! Failed to move '" + System.IO.Path.GetFileName(filename) + "' because of '" + ex.Message);
      }

    }

    private DateTime getDateFromImageFile(string FileName)
    {
      System.Drawing.Image img = null;
      DateTime dt = new DateTime();
      string sdate = string.Empty;

      using (System.IO.FileStream stream = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
      {
        img = Image.FromStream(stream);

        if (Array.IndexOf(img.PropertyIdList, (Int32)PropertyTag.DateTimeOriginal) > -1)
        {
          sdate = System.Text.Encoding.UTF8.GetString(img.GetPropertyItem((Int32)PropertyTag.DateTimeOriginal).Value).Trim();
        }
        else if (Array.IndexOf(img.PropertyIdList, (Int32)PropertyTag.DateTime) > -1)
        {
          sdate = System.Text.Encoding.UTF8.GetString(img.GetPropertyItem((Int32)PropertyTag.DateTime).Value).Trim();
        }
        else if (Array.IndexOf(img.PropertyIdList, (Int32)PropertyTag.DateTimeDigitized) > -1)
        {
          sdate = System.Text.Encoding.UTF8.GetString(img.GetPropertyItem((Int32)PropertyTag.DateTimeDigitized).Value).Trim();
        }
        else
        {
          System.Diagnostics.Debug.WriteLine(false, "No date/time fields populated in image.");
        }

        if (!string.IsNullOrEmpty(sdate))
        {
          string secondhalf = sdate.Substring(sdate.IndexOf(" "), (sdate.Length - sdate.IndexOf(" ")));
          string firsthalf = sdate.Substring(0, 10);
          firsthalf = firsthalf.Replace(":", "-");

          sdate = firsthalf + secondhalf;
          dt = DateTime.Parse(sdate);
        }
      }

      // There are some cases where the date/time returned are actually bogus, and in those cases we'd perhaps want to grab the actual file "date modified"
      // property rather than the EXIF.
      // Problem is, how to know when the date/time returned in EXIF is actually bogus?
      //     I've seen the LG Nexus 4 return date/times of 12/08/2002 @ 12:00, and then for a picture taken seconds earlier or later it's the correct time.
      //     I've seen a childs digital camera return a generic datetime of "2007-05-16 12:20:23" where the date is bogus, but the time is time elapsed since camera turned on.
      //
      // Possible rules to consider:
      //        Don't use the regular DateTime property at all?  Ignore it.  Don't use file date time in this case either since the camera has no date/time configuration.
      //        Watch out for date/time combinations where the time is 12:00 (exactly noon) and >1 year in the past.  If found, fall back to file date/time?
      if ((DateTime.Now.Subtract(dt).Days) > (365 * 4))
      {
        // detected date is >4 years old.  Good chance the date is garbage.  Use File Modified date instead.
        dt = System.IO.File.GetLastWriteTime(FileName);
      }

      return dt;
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
