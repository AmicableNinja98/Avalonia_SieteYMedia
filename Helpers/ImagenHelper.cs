using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
namespace SieteYMedia.Helpers;

public static class ImagenHelper
{
    public static Bitmap LoadFromResource(Uri resourceUri) => new Bitmap(AssetLoader.Open(resourceUri));
}