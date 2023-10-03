using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;

using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Graphics;
using Android.Graphics;
using System.Drawing;
using System.IO;
using static Android.Renderscripts.ScriptGroup;
using Android.Media;
using Java.Security.Cert;
using Android.Util;

namespace PrintingAPIsExerciser;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        //READING A PNG FROM ASSETT
        const int maxReadSize = 256 * 1024;
        byte[] content;
        AssetManager assets = this.Assets;
        using (BinaryReader br = new BinaryReader(assets.Open("cxnt48.png")))
        {
            content = br.ReadBytes(maxReadSize);
        }

        MemoryStream mStream = new MemoryStream();
        mStream.Write(content, 0, content.Length);
        mStream.Flush();
        mStream.Position = 0;

        Console.WriteLine(content.Length);

        ZebraImageI zii = ZebraImageFactory.GetImage( mStream );

        MemoryStream outStr = new MemoryStream();

        PrinterUtil.ConvertGraphic("E:CXNT48.PNG", zii, outStr);

        Console.WriteLine(outStr.Length);
        outStr.Position = 0;

        using (var fileStream = new FileStream("/enterprise/usr/cxnt48-zebra.png", FileMode.Create, FileAccess.Write))
        {
            outStr.CopyTo(fileStream);
            fileStream.Close();
        }

        outStr.Position = 0;
        for (int i = 0; i < 100; i++)
            Log.Info("OUTSTREAM", ""+Convert.ToChar(outStr.ReadByte()));
        


    }
}
