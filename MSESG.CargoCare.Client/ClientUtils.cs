using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Client
{

  public class ClientUtils
  {
    private static string _UpdateUrl = "http://updater.cc2.mangux.com/MSESGCargoCareSystemUpdater.xml";
    public ClientUtils()
    {

    }

    public static void CheckForUpdates()
    {
      AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("es-Es");

      //If you want to open download page when user click on download button uncomment below line.

      //AutoUpdater.OpenDownloadPage = true;

      //Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

      AutoUpdater.LetUserSelectRemindLater = false;
      AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Hours;
      AutoUpdater.RemindLaterAt = 6;
      AutoUpdater.Start(_UpdateUrl);
    }

    internal static string GetMainFormTitle()
    {
      return "Cargo Care System";
    }
  }
}
