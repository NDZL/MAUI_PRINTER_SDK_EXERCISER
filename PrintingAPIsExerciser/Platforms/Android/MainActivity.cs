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
        //Android.Media.Image img = (Android.Graphics.Bitmap)Android.Media.Image.FromArray(content);

        Console.WriteLine(content.Length);

        ZebraImageI zii = ZebraImageFactory.GetImage( mStream );

        MemoryStream outStr = new MemoryStream();

        PrinterUtil.ConvertGraphic("E:CXNT48.PNG", zii, outStr);

        Console.WriteLine(outStr.Length);




    }
}
